using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Domain.Tests
{
    [TestFixture]
    public class InvoiceRowTest
    {
        private IDictionary<FeeType, Fee> _fees = new Dictionary<FeeType, Fee>
        {
            {
                FeeType.OneTime,
                new Fee
                {
                    FeeValue = 100
                }
            },
            {
                FeeType.PremiumDaily,
                new Fee
                {
                    FeeValue = 60
                }
            },
            {
                FeeType.RegularDaily,
                new Fee
                {
                    FeeValue = 40
                }
            },
        };

        [Test]
        public void HeavyEquipmentPriceAndLoyaltyPointsTest()
        {
            const int days = 3;

            var invoiceRow = PrepareInvoiceRowWithEquipment(InventoryType.Heavy, days);

            const decimal expectedPrice = 280;

            Assert.AreEqual(expectedPrice, invoiceRow.GetRowPrice(_fees), "Prices are different");

            const int expectedPoints = 2;

            Assert.AreEqual(expectedPoints, invoiceRow.GetRowLoyaltyPoints(), "Points are different");
        }

        [Test]
        public void RegularEquipmentPriceAndLoyaltyPointsTest()
        {
            const int days = 4;

            var invoiceRow = PrepareInvoiceRowWithEquipment(InventoryType.Regular, days);

            const decimal expectedPrice = 300;

            Assert.AreEqual(expectedPrice, invoiceRow.GetRowPrice(_fees), "Prices are different");

            const int expectedPoints = 1;

            Assert.AreEqual(expectedPoints, invoiceRow.GetRowLoyaltyPoints(), "Points are different");
        }

        [Test]
        public void SpecializedEquipmentPriceAndLoyaltyPointsTest()
        {
            const int days = 5;

            var invoiceRow = PrepareInvoiceRowWithEquipment(InventoryType.Specialized, days);

            const decimal expectedPrice = 260;

            Assert.AreEqual(expectedPrice, invoiceRow.GetRowPrice(_fees), "Prices are different");

            const int expectedPoints = 1;

            Assert.AreEqual(expectedPoints, invoiceRow.GetRowLoyaltyPoints(), "Points are different");
        }

        private InvoiceRow PrepareInvoiceRowWithEquipment(InventoryType type, int rentedDays)
        {
            var row = new InvoiceRow();
            row.Equipment = new Equipment
            {
                Type = type
            };
            row.RentedDays = rentedDays;
            return row;
        }
    }
}
