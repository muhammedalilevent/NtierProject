using NTier.AuthService.Models;
using NTier.Model.Entities;
using NTier.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NTier.AuthService.Controllers
{
    public class AuthController : ApiController
    {
        AppUserService _userService;
        public AuthController()
        {
            _userService = new AppUserService();
        }

        [HttpPost]
        public HttpResponseMessage Login(Credentials model)
        {
            var url = "";
            if (model.username == null ||model.password == null)
            {
                url = "http://localhost:7865/Home/Login";
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Success = true, RedirectUrl = url });
            }
            if (_userService.CheckCredentials(model.username,model.password))
            {
                AppUser p = new AppUser();
                p = _userService.FindByUserName(model.username);
                if (p.Role == Role.Admin ||p.Role == Role.Member)
                {
                    url = "http://localhost:7865/Home/Index/" + p.Id;
                    return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = url });
                }
                else
                {
                    url = "http://localhost:7865/Home/Index";
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Success = true, RedirectUrl = url });
                }
            }
            url = "http://localhost:7865/Home/Index";
            return Request.CreateResponse(HttpStatusCode.BadRequest, new { Success = true, RedirectUrl = url });
        }

        [HttpGet]
        public HttpResponseMessage Logout()
        {
            var url = "http://localhost:7865/Home/Logout";
            return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = url });
        }
    }
}
