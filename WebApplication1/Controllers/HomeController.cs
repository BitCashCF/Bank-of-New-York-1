using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index(string id)
        {
            return View("Index", (object)id);
        }

        public ActionResult OrderedProducts(string id)
        {
            var orders = OrdersDb.GetOrders;
            if (!String.IsNullOrEmpty(id) && id != "All")
            {
                orders = orders.Where(o => o.Customer == id);
            }

            return PartialView(orders);
        }
    }
}