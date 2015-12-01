using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvoiceRow
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EquipmentId { get; set; }
        private int _rentedDays;
        [NotMapped]
        public int RentedDays {
            get
            {
                if (_rentedDays > 0)
                    return _rentedDays;
                
                _rentedDays = (EndDate - StartDate).Days;
                return _rentedDays;
            }
            set
            {
                _rentedDays = value;
            }
        }

        public virtual Invoice Invoice { get; set; }
        public virtual Equipment Equipment { get; set; }

        private decimal _price;
        
        public decimal GetRowPrice(IDictionary<FeeType, Fee> availableFees)
        {
            if (_price > 0)
                return _price;

            if (availableFees == null)
                throw new Exception("Fees were not passed, row price was not calculated");

            if (!(availableFees.ContainsKey(FeeType.OneTime)
                && availableFees.ContainsKey(FeeType.PremiumDaily)
                && availableFees.ContainsKey(FeeType.RegularDaily)))
                throw new Exception("Not all required fees have been passed");
            
            switch (Equipment.Type)
            {
                case InventoryType.Heavy:
                    _price = availableFees[FeeType.OneTime].FeeValue +
                            RentedDays * availableFees[FeeType.PremiumDaily].FeeValue;
                    break;
                case InventoryType.Regular:
                    _price = availableFees[FeeType.OneTime].FeeValue;
                    _price += AddPremiumAndRegularFees(2, availableFees);
                    break;
                case InventoryType.Specialized:
                    _price = AddPremiumAndRegularFees(3, availableFees);
                    break;
                default:
                    _price = 0;
                    break;
            }
            return _price;
        }

        private decimal AddPremiumAndRegularFees(int premiumDays, IDictionary<FeeType, Fee> availableFees)
        {
            decimal price;

            var regularDays = RentedDays - premiumDays;
            if (regularDays > 0)
                price = premiumDays * availableFees[FeeType.PremiumDaily].FeeValue
                         + regularDays * availableFees[FeeType.RegularDaily].FeeValue;
            else
                price = RentedDays * availableFees[FeeType.PremiumDaily].FeeValue;

            return price;
        }

        public int GetRowLoyaltyPoints()
        {
            switch (Equipment.Type)
            {
                case InventoryType.Heavy:
                    return 2;
                case InventoryType.Regular:
                case InventoryType.Specialized:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}
