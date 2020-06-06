using NTier.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTierProje.UI.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            _orderDetailServise = new OrderDetailServise();
            _orderService = new OrderService();
            _productService = new ProductService();
        }

        private OrderService _orderService;

        public OrderService OrderService
        {
            get { return _orderService; }
            set { _orderService = value; }
        }

        private ProductService _productService;

        public ProductService ProductService
        {
            get { return _productService; }
            set { _productService = value; }
        }

        private OrderDetailServise _orderDetailServise;

        public OrderDetailServise OrderDetailServise
        {
            get { return _orderDetailServise; }
            set { _orderDetailServise = value; }
        }



    }
}