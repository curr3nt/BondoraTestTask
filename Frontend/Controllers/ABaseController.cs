﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DTO;
using Frontend.BackendWrapper;
using Frontend.CustomExceptions;
using Frontend.Properties;

namespace Frontend.Controllers
{
    public abstract class ABaseController : Controller
    {
        protected const string InventoryListKey = "InventoryList";
        protected IBackendServiceWrapper Service;

        protected ABaseController()
        {
            var ip = Settings.Default.ServerIp;
            var port = Settings.Default.ServerPort;
            Service = new BackendTcpServiceWrapper(ip, port);
        }

        protected ICollection<EquipmentDto> GetAndCacheInventoryList()
        {
            ICollection<EquipmentDto> inventoryList;
            if (HttpRuntime.Cache[InventoryListKey] != null)
                inventoryList = HttpRuntime.Cache[InventoryListKey] as ICollection<EquipmentDto>;
            else
            {
                inventoryList = Service.GetInventory();
                HttpRuntime.Cache.Insert(InventoryListKey, inventoryList);
            }

            return inventoryList;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            if (exception is ABackendException)
            {
                var message = exception.Message;
                TempData["ErrorMessage"] = message;
                filterContext.ExceptionHandled = true;
                filterContext.Result 
                    = new RedirectToRouteResult(new RouteValueDictionary(
                        new { controller = "Home", action = "Index" }));
            }
        }
    }
}
