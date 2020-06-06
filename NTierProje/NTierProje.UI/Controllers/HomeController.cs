using NTier.Model.Entities;
using NTier.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NTierProje.UI.Controllers
{
    public class HomeController : Controller
    {
        ProductService _productService;
        AppUserService _appUserService;
        CategoryService _categoryService;
        public HomeController()
        {
            _productService = new ProductService();
            _appUserService = new AppUserService();
            _categoryService = new CategoryService();
        }
        // GET: Home
        public ActionResult Index(Guid? id)
        {
            if (id != null)
            {
                AppUser user = new AppUser();
                user = _appUserService.GetById((Guid)id);
                string cookie = user.UserName.ToString();
                FormsAuthentication.SetAuthCookie(cookie, true);
                if (user.Role == Role.Admin)
                {
                    return Redirect("/Admin/Home/Index");
                }
            }
            var model = _productService.GetDefault(x => x.UnitsInStock > 0).OrderByDescending(x => x.CreatedDate).Take(16).ToList();

            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        }

        [ChildActionOnly]
        public ActionResult CategoryList()
        {
            return PartialView("_CategoryList", _categoryService.GetActive());
        }

        [ChildActionOnly]
        public ActionResult ProductList()
        {
            return PartialView("_ProductList", _productService.GetActive().OrderByDescending(x=>x.CreatedDate).Take(9).ToList());
        }

    }
}