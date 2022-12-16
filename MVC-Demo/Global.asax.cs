using CaptchaMvc.Infrastructure;
using CaptchaMvc.Interface;
using CaptchaMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVC_Demo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var captchaManager = (DefaultCaptchaManager)CaptchaUtils.CaptchaManager;
            //-- this will generate  alphanumeric string------------------
            captchaManager.CharactersFactory = () => "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            captchaManager.PlainCaptchaPairFactory = length =>
            {
                string randomText = RandomText.Generate(captchaManager.CharactersFactory(), length);
                bool ignoreCase = false;//This parameter is responsible for ignoring case.
                return new KeyValuePair<string, ICaptchaValue>(Guid.NewGuid().ToString("N"),
                    new StringCaptchaValue(randomText, randomText, ignoreCase));
            };

        }
    }
}
