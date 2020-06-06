﻿using NTier.Model.Entities;
using NTier.Service.Option;
using NTierProje.UI.Areas.Member.Models;
using NTierProje.UI.Areas.Member.Models.VM;
using NTierProje.UI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTierProje.UI.Areas.Member.Controllers
{
    [CustomAuthorize(Role.Admin, Role.Member)]
    public class CartController : Controller
    {
        ProductService _productService;
        public CartController()
        {
            _productService = new ProductService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            if (Session["sepet"] != null)
            {
                ProductCart cart = Session["sepet"] as ProductCart;
                List<ProductVM> productList = cart.CartProductList.ToList();
                return Json(productList, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(Guid id)
        {
            Product product = _productService.GetById(id);
            ProductVM model = new ProductVM
            {
                Id = product.Id,
                ProductName = product.Name,
                UnitPrice = product.Price,
                UnitysInStock = product.UnitsInStock,
                Quantity = 1,
                ImagePath = product.ImagePath
            };

            if (Session["sepet"] != null)
            {
                ProductCart cart = Session["sepet"] as ProductCart;
                cart.AddCart(model);
                Session["sepet"] = cart;
            }
            else
            {
                ProductCart cart = new ProductCart();
                cart.AddCart(model);
                Session.Add("sepet", cart);
            }

            return Json("");
        }

        public JsonResult DecreaseCart(Guid id)
        {
            if (Session["sepet"] != null)
            {
                ProductCart cart = Session["sepet"] as ProductCart;
                cart.DecreaseCart(id);
                Session["sepet"] = cart;
            }
            return Json("");
        }

        public JsonResult IncreaseCart(Guid id)
        {
            ProductCart cart = Session["sepet"] as ProductCart;
            cart.IncreaseCart(id);
            Session["sepet"] = cart;
            return Json("");
        }

        public JsonResult RemoveCart(Guid id)
        {
            ProductCart cart = Session["sepet"] as ProductCart;
            cart.RemoveCart(id);
            Session["sepet"] = cart;
            return Json("");
        }
    }
}