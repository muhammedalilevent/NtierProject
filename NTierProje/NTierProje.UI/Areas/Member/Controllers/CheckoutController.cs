﻿using NTier.Model.Entities;
using NTier.Service.Option;
using NTierProje.UI.Areas.Member.Models;
using NTierProje.UI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTierProje.UI.Areas.Member.Controllers
{
    [CustomAuthorize(Role.Admin,Role.Member)]
    public class CheckoutController : Controller
    {
        OrderService _orderService;
        ProductService _productService;
        AppUserService _appUserService;

        public CheckoutController()
        {
            _orderService = new OrderService();
            _productService = new ProductService();
            _appUserService = new AppUserService();
        }

        public ActionResult Add()
        {
            if (Session["sepet"] == null)
            {
                return Redirect("/Home/Index");
            }

            ProductCart cart = Session["sepet"] as ProductCart;
            Orders o = new Orders();

            AppUser user = _appUserService.FindByUserName(HttpContext.User.Identity.Name);
            o.AppUserID = user.Id;
            o.AppUser = user;
            _appUserService.DetachEntity(user);

            Product p = new Product();
            foreach (var item in cart.CartProductList)
            {
                p = _productService.GetById(item.Id);
                p.UnitsInStock -= item.Quantity;
                _productService.Update(p);
                o.OrderDetails.Add(new OrderDetails
                {
                    Product = p,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
                _productService.DetachEntity(p);
            }
            _productService.DetachEntity(p);
            _orderService.Add(o);

            return Redirect("/Home/Index");
        }
    }
}