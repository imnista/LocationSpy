namespace LocationSpy.Controllers
{
    #region using directives

    using Services;
    using System.Web.Mvc;

    #endregion using directives

    public class TrackerController : BaseController
    {
        public static TrackerService TrackerService = new TrackerService();

        public ActionResult Index()
        {
            return this.View();
        }
    }
}