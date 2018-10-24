namespace LocationSpy.Controllers
{
    #region using directives

    using System;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using Models;
    using Services;

    #endregion

    public class LocatorController : BaseController
    {
        public static LocatorService LocatorService = new LocatorService();

        [HttpGet]
        public ActionResult Index()
        {
            this.ViewBag.Top30Items = LocatorService.GetAllByPageSortBy(0, 30, m => m.LastModifiedTime, true);
            return this.View();
        }

        [HttpPost]
        public ActionResult Submit(LocatorItem model)
        {
            if (this.ModelState.IsValid)
            {
                // this.ModelState.AddModelError("", "");
                model.Identifier = Guid.NewGuid().ToString("N");
                model.Status = CurrentStatus.NotReady;
                model.Creator = "Anonymous";
                model.LastModifiedTime = model.CreatedTime = DateTime.UtcNow;
                LocatorService.AddAsync(model);
                return this.RedirectToAction("Detail", new {id = model.Identifier});
            }
            return this.View("Index", model);
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            this.ViewBag.Id = id;
            return this.View();
        }

        [HttpGet]
        public ActionResult Check(string id)
        {
            var model = LocatorService.Get(m => m.Identifier == id);
            return this.Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SuggestTitle(string url)
        {
            try
            {
                if (Uri.TryCreate(url, UriKind.Absolute, out var targetUrl)
                    && (targetUrl.Scheme == Uri.UriSchemeHttp || targetUrl.Scheme == Uri.UriSchemeHttps))
                {
                    using (var webClient = new WebClient
                    {
                        Encoding = Encoding.UTF8
                    })
                    {
                        var source = webClient.DownloadString(targetUrl);
                        var title = Regex
                            .Match(source, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase)
                            .Groups["Title"].Value;
                        return this.Json(
                            new {status = 0, title},
                            JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch
            {
                // ignored
            }
            return this.Json(
                new {status = 0, title = ""},
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            LocatorService.Delete(m => m.Identifier == id);
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Clear(string passphrase)
        {
            if (passphrase == "hy19901018")
            {
                LocatorService.Delete(m => true);
            }
            return this.RedirectToAction("Index");
        }
    }
}