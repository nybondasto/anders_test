using ScoreApp.Models;
using System.Web.Mvc;


namespace ScoreApp.Controllers
{
    public class HomeController : Controller
    {
        Scores _list = new Scores();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Here you can find my contact information.";
            return View();
        }

        public ActionResult List()
        {
            Scores lst = populateScores();
            return View(lst);

        }

        private Scores populateScores()
        {
            DL oDL = new DL();
            Scores lst = oDL.GetScores();
            return lst;
        }
    }
}