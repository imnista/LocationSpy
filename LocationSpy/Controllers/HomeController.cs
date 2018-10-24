namespace LocationSpy.Controllers
{
    #region using directives

    using System.Web.Mvc;

    #endregion

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult About()
        {
            return this.View();
        }
    }
}