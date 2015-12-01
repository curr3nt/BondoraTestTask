using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DTO;

namespace Frontend.Controllers
{
    public class CartController : ABaseController
    {
        private const string CartKey = "cart";

        public ActionResult AddToCart(int? equipmentId, int? daysRented)
        {
            if (!equipmentId.HasValue || equipmentId == 0 
                || !daysRented.HasValue || daysRented == 0)
                return Json(false, JsonRequestBehavior.AllowGet);

            var cart = GetOrInitCartFromSession();
            
            var inventoryList = GetAndCacheInventoryList();
            var equipmentName = inventoryList.Single(eq => eq.Id == equipmentId.Value).Name;

            var cartRow = new CartRowDto
            {
                EquipmentId = equipmentId.Value,
                EquipmentName = equipmentName,
                DaysRented = daysRented.Value
            };
            cart.Rows.Add(cartRow);

            return Json(cart.Rows.Count, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisplayCart()
        {
            var cart = GetOrInitCartFromSession();

            return View("EditCart", cart);
        }

        public ActionResult ClearCart()
        {
            Session[CartKey] = null;
            var cart = GetOrInitCartFromSession();
            TempData["SuccessMessage"] = "Cart has been cleared";

            return View("EditCart", cart);
        }

        private CartDto GetOrInitCartFromSession()
        {
            CartDto cart;

            if (Session[CartKey] == null)
            {
                cart = new CartDto();
                cart.Rows = new List<CartRowDto>();
                Session[CartKey] = cart;
            }
            else
                cart = (CartDto)Session[CartKey];

            return cart;
        }

        public ActionResult ConfirmCart(CartDto cart)
        {
            if (cart.Rows == null || cart.Rows.Count == 0)
            {
                TempData["ErrorMessage"] = "Submitted an empty cart";
                return View("EditCart", GetOrInitCartFromSession());
            }

            var invoiceFile = Service.ConfirmCart(cart);

            var encoding = Encoding.UTF8;
            var bytes = invoiceFile.FileRows.SelectMany(row => encoding.GetBytes(row + Environment.NewLine)).ToArray();
            
            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("Invoice{0}.csv", invoiceFile.InvoiceNumber),
                Inline = false
            };
            Response.AppendHeader("Content-Disposition", contentDisposition.ToString());

            return File(bytes, contentDisposition.ToString());
        }
    }
}
