using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;

namespace Frontend.Controllers
{
    public class CartController : Controller
    {

        private const string CartKey = "cart";

        public ActionResult AddToCart(int? equipmentId, int? daysRented)
        {
            if (!equipmentId.HasValue || equipmentId == 0 
                || !daysRented.HasValue || daysRented == 0)
                return Json(false, JsonRequestBehavior.AllowGet);

            CartDto cart;

            if (Session[CartKey] == null)
            {
                cart = new CartDto();
                cart.Rows = new List<CartRowDto>();
                Session[CartKey] = cart;
            }
            else
                cart = (CartDto) Session[CartKey];

            var cartRow = new CartRowDto {EquipmentId = equipmentId.Value, DaysRented = daysRented.Value};
            cart.Rows.Add(cartRow);

            return Json(cart.Rows.Count, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClearCart()
        {
            Session[CartKey] = null;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}
