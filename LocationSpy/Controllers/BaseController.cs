namespace LocationSpy.Controllers
{
    #region using directives

    using Models;
    using System.Web.Mvc;

    #endregion using directives

    public class BaseController : Controller
    {
        protected PlatformType GetUserPlatform()
        {
            var ua = this.HttpContext.Request.UserAgent;
            if (string.IsNullOrEmpty(ua))
            {
                return PlatformType.Unknown;
            }

            ua = ua.ToLowerInvariant();
            if (ua.Contains("android"))
            {
                return PlatformType.Android;
            }
            if (ua.Contains("iphone") || ua.Contains("ipad"))
            {
                return PlatformType.Apple;
            }
            if (this.HttpContext.Request.Browser.Platform == "WinNT")
            {
                return PlatformType.Windows;
            }
            return PlatformType.Unknown;
        }
    }
}