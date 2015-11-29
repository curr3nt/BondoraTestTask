using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain;
using DTO;

namespace BusinessLogic
{
    /// <summary>
    /// This implementataion of IBusinessService interface is strongly coupled with EF's DbContext
    /// </summary>
    public class BusinessService : IBusinessService
    {
        public ICollection<EquipmentDto> GetInventoryList()
        {
            using (var context = new DalContext())
            {
                var equipments = context.Equipments;

                var equipmentDtos = equipments.Select(eq => new EquipmentDto {Id = eq.Id, Name = eq.Name}).ToList();
                
                return equipmentDtos;
            }
        }

        public InvoiceFileDto ConfirmCart(CartDto cart)
        {
            InvoiceFileDto fileDto;
            using (var context = new DalContext())
            {
                var invoice = SaveInvoice(cart, context);
                
                var fees = context.Fees.ToDictionary(f => f.Type, f => f);
                fileDto = PrepareInvoiceFile(invoice, fees);
            }
            return fileDto;
        }

        #region Invoice saving

        private static Invoice SaveInvoice(CartDto cart, DalContext context)
        {
            var invoice = new Invoice();
            invoice.Customer = cart.CustomerName;
            invoice.Date = DateTime.Now;

            invoice.Rows = cart.Rows.Select(cr => GetInvoiceRow(cr, invoice)).ToList();
                
            var lastId = GetLastInvoiceId(context);
            invoice.Number = string.Format("#{0:D6}", lastId + 1);

            context.Invoices.Add(invoice);
            context.SaveChanges();

            return invoice;
        }

        private static InvoiceRow GetInvoiceRow(CartRowDto cartRow, Invoice invoice)
        {
            var invoiceRow = new InvoiceRow();
            invoiceRow.Invoice = invoice;
            invoiceRow.StartDate = DateTime.Now;
            invoiceRow.EndDate = invoiceRow.StartDate.AddDays(cartRow.DaysRented);
            invoiceRow.EquipmentId = cartRow.EquipmentId;
            
            return invoiceRow;
        }

        private static int GetLastInvoiceId(DalContext context)
        {
            var invoices = context.Invoices;

            var maxId = invoices.Where(i => i.Id == invoices.Max(inv => inv.Id)).Select(i => i.Id).SingleOrDefault();

            return maxId;
        }

        #endregion

        #region Invoice file preparation

        /// <summary>
        /// This method is not good at all, since it contains hard-coded strings.
        /// If there will be need to localize(translate) the file, this code should be rewritten.
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="fees"></param>
        /// <returns></returns>
        private InvoiceFileDto PrepareInvoiceFile(Invoice invoice, IDictionary<FeeType, Fee> fees)
        {
            var fileDto = new InvoiceFileDto();

            const string invoiceHeader = "Invoice";
            var number = string.Format("Number,{0}", invoice.Number);
            var date = string.Format("Date,{0}", invoice.Date);
            var customer = string.Format("Customer,{0}", invoice.Customer);
            AddToInvoiceFile(true, fileDto, invoiceHeader, number, date, customer);

            const string rowsHeader = "Rows";
            const string rowsDescription = "Equipments,Days rented,Price";
            AddToInvoiceFile(false, fileDto, rowsHeader, rowsDescription);
            
            var rowStrs = invoice.Rows.Select(row => 
                string.Format("{0},{1},{2}", row.RentedEquipment, row.RentedDays, row.GetRowPrice(fees))).ToArray();
            AddToInvoiceFile(true, fileDto, rowStrs);

            const string bottomHeader = "Total";
            var totalPrice = string.Format("Price,{0}", invoice.GetInvoicePrice());
            var points = string.Format("Loyalty points,{0}", invoice.GetInvoiceLoyaltyPoints());
            AddToInvoiceFile(true, fileDto, bottomHeader, totalPrice, points);

            return fileDto;
        }

        private void AddToInvoiceFile(bool addDelimiter, InvoiceFileDto fileDto, params string[] rows)
        {
            foreach (var row in rows)
                fileDto.FileRows.Add(row);
            // section delimiter
            if (addDelimiter)
                fileDto.FileRows.Add(string.Empty);
        }

        #endregion
    }
}
