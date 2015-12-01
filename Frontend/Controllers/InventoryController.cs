using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using DTO;

namespace Frontend.Controllers
{
    public class InventoryController : ABaseController
    {
        //
        // GET: /Inventory/

        /// <summary>
        /// Method gets invetory lsit from backend service. Caches received result.
        /// Is caching here is ok?
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var inventoryList = GetAndCacheInventoryList();

            return View("InventoryList", inventoryList);
        }

    }
}
