﻿using NTier.Model.Entities;
using NTier.Service.Option;
using NTierProje.UI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTierProje.UI.Areas.Admin.Controllers
{
    [CustomAuthorize(Role.Admin)]
    public class HomeController : Controller
    {
        OrderService _orderService;
        public HomeController()
        {
            _orderService = new OrderService();
        }
        // GET: Admin/Home
        public ActionResult Index()
        {
            //Onaylanmış tüm siparişler admine gönderiliyor.
            List<Orders> model = _orderService.GetDefault(x => x.Confirmed == false && x.Status == NTier.Core.Entity.Enum.Status.Active);

            ViewBag.Siparis = model.Count;

            return View();
        }
    }
}