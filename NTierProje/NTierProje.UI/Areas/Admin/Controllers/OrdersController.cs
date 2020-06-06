using NTier.Model.Entities;
using NTier.Service.Option;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTierProje.UI.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        OrderService _orderService;
        OrderDetailServise _orderDetailServise;
        ProductService _productService;

        public OrdersController()
        {
            _orderService = new OrderService();
            _orderDetailServise = new OrderDetailServise();
            _productService = new ProductService();
        }

        public ActionResult List(int page = 1)
        {
            //Daha Onaylanmamış tüm siparişleri listele
            List<Orders> model = _orderService.ListPendingOrders(); 
            return View(model.ToPagedList(page,10));
        }

        public JsonResult OrderCount()
        {
            int count = _orderService.PendingOrderCount();
            return Json(count,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(Guid Id)
        {
            List<OrderDetails> model = _orderDetailServise.GetDefault(x=>x.Orders.Id == Id);
            return View(model);
        }

        public RedirectResult ConfirmOrder(Guid id)
        {
            Orders order = new Orders();
            order = _orderService.GetById(id);
            order.Confirmed = true;
            _orderService.Update(order);
            return Redirect("/Admin/Orders/List");
        }

        public RedirectResult RejectOrder(Guid id)
        {
            Orders order = _orderService.GetById(id);

            foreach (var item in order.OrderDetails)
            {
                Product p = _productService.GetById(item.Product.Id);
                p.UnitsInStock += Convert.ToInt16(item.Product.Quantity);
                p.Status = NTier.Core.Entity.Enum.Status.Deleted;
                _productService.Update(p);
            }
            order.Confirmed = false;
            order.Status = NTier.Core.Entity.Enum.Status.Deleted;
            _orderService.Update(order);
            return Redirect("/Admin/Orders/List");
        }

    }
}