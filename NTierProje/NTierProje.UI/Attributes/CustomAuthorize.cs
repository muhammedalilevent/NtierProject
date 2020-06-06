using NTier.Model.Entities;
using NTier.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTierProje.UI.Attributes
{
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class,Inherited = true,AllowMultiple = true)]// Allow Multiplw ile birden fazla rol girişi yapılabiliyor.
    public class CustomAuthorize : AuthorizeAttribute
    {
        private string[] UserProfilesRequired { get; set; }
        public CustomAuthorize(params object[] userProfilesRequired)
        {
            if (userProfilesRequired.Any(x => x.GetType().BaseType != typeof(Enum)))
                throw new ArgumentException("userProfilesRequired");

            this.UserProfilesRequired = userProfilesRequired.Select(x => Enum.GetName(x.GetType(), x)).ToArray();
        }

        public override void OnAuthorization(AuthorizationContext context)
        {
            bool authorized = false;

            AppUserService service = new AppUserService();

            AppUser user = service.FindByUserName(HttpContext.Current.User.Identity.Name);
            string userRole = Enum.GetName(typeof(Role), user.Role);
            foreach (var role in this.UserProfilesRequired)
            {
                if (userRole == role)
                {
                    authorized = true;
                    break;
                }
            }
            if (!authorized)
            {
                var url = new UrlHelper(context.RequestContext);
                var logonUrl = url.Action("Index", "Home", new { Id = 302, Area = "" });
                context.Result = new RedirectResult(logonUrl);
                return; 
            }
        }
    }
}