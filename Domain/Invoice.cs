using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }

        public virtual ICollection<InvoiceRow> Rows { get; set; }

        private decimal _price;
        private int _points;

        /// <summary>
        /// Method assumes that individual row prices were already calculated beforehand
        /// If prices were not calculated, underlying GetRowPrice(fees) method will throw an exception
        /// </summary>
        /// <returns></returns>
        public decimal GetInvoicePrice()
        {
            if (_price > 0)
                return _price;

            _price = Rows.Sum(r => r.GetRowPrice(null));

            return _price;
        }

        public int GetInvoiceLoyaltyPoints()
        {
            if (_points > 0)
                return _points;

            _points = Rows.Sum(r => r.GetRowLoyaltyPoints());

            return _points;
        }
    }
}
