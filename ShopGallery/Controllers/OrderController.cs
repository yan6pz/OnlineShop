using BusinessLayer;
using DAL;
using ShopGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopGallery.Controllers
{
    public class OrderController : Controller
    {
        

        public ActionResult ShowOrders()
        {
            var databaseAccessInstance = new DatabaseAccess();
            var orderModel = new PopulateOrder();
            IOrderable order = new OrderActions(databaseAccessInstance, orderModel);
            var orders=order.ShowOrders(Session["Email"].ToString());
            return View(orders);
        }
        public ActionResult Delete(int id)
        {
            var databaseAccessInstance = new DatabaseAccess();
            var orderModel = new PopulateOrder();
            IOrderable order = new OrderActions(databaseAccessInstance, orderModel);
            order.Delete(id);
            return RedirectToAction("ShowOrders", "Order");
        }
        public ActionResult ShowOrderDetails()
        {
            var databaseAccessInstance = new DatabaseAccess();
            var orderModel = new PopulateOrderDetails();
            IOrderDetail order = new OrderDetailActions(databaseAccessInstance, orderModel);
            var result = order.Details(Session["Email"].ToString());
            var firstOrDefault = result.FirstOrDefault();
            Session["IsAdmin"] = firstOrDefault != null && firstOrDefault.CurrentUser.IsAdmin;
            return View(result);
        }

    }
}
