namespace LocationSpy.Controllers
{
    #region using directives

    using Models;
    using Services;
    using SquirrelFramework.Domain.Model;
    using System;
    using System.IO;
    using System.Net;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    #endregion using directives

    public class VisitController : BaseController
    {
        private static readonly JavaScriptSerializer serializer = new JavaScriptSerializer();

        public static LocatorService LocatorService = new LocatorService();

        [HttpGet]
        public ActionResult Index(string id)
        {
            var model = LocatorService.Get(m => m.Identifier == id);
            model.LastModifiedTime = DateTime.UtcNow;
            model.PlatformType = base.GetUserPlatform();
            model.IPAddress = this.Request.UserHostAddress;
            model.Status = CurrentStatus.FoundByIPAddress;
            LocatorService.Update(model);
            return this.View(model);
        }

        [HttpGet]
        public ActionResult BaiduIPLocationServerProxy(string ticks)
        {
            var request = WebRequest.CreateHttp($"https://map.baidu.com/?qt=ipLocation&t={ticks}");
            request.Method = "GET";
            request.Referer = "https://map.baidu.com/";
            request.Host = "map.baidu.com";
            request.ContentType = "application/json";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36";
            // It's important to set the cookie.
            request.CookieContainer = new CookieContainer(1);
            request.CookieContainer.Add(new Cookie("BAIDUID", "12B39C1E49D9154C0378CAB78FD5745F:FG=1", "/", ".baidu.com"));

            // Set the Client IP for Baidu IP-Geolocation service
            // It's NOT working right now
            // TODO:
            request.Headers.Add("X-Forwarded-For", this.Request.UserHostAddress);
            request.Headers.Add("X-FORWARDED-FOR", this.Request.UserHostAddress);
            request.Headers.Add("X_FORWARDED_FOR", this.Request.UserHostAddress);
            request.Headers.Add("HTTP_X_FORWARDED_FOR", this.Request.UserHostAddress);
            request.Headers.Add("X-Real-IP", this.Request.UserHostAddress);
            request.Headers.Add("REMOTE_ADDR", this.Request.UserHostAddress);

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                var reader = new StreamReader(response.GetResponseStream());
                var result = reader.ReadToEnd();
                return this.Json(serializer.DeserializeObject(result),
                     JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult SyncLoc(string id, double longitude, double latitude, string message)
        {
            var model = LocatorService.Get(m => m.Identifier == id);
            model.LastModifiedTime = DateTime.UtcNow;
            model.PlatformType = base.GetUserPlatform();
            model.IPAddress = this.Request.UserHostAddress;
            model.Status = CurrentStatus.FoundByGPS;
            model.Location = new Geolocation(longitude, latitude);
            model.AuxiliaryLocationInformation = message;
            LocatorService.Update(model);
            return this.Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}