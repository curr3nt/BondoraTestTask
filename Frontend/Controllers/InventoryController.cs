using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class InventoryController : ABaseController
    {
        //
        // GET: /Inventory/

        public ActionResult List()
        {
            var inventoryList = Service.GetInventory();

            return View("InventoryList", inventoryList);
        }

    }
}
