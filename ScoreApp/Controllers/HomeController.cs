using ScoreApp.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace ScoreApp.Controllers
{
    public class HomeController : Controller
    {
        Scores _list = new Scores();

        public ActionResult Index()
        {
            TempData["sortOrder"] = "DESC";
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



        //-- Default view for score listing
        public ActionResult List()
        {
            Scores lst = populateScores();
            return View(lst);
        }


        //-- Method for sorting scores
        [HttpPost]
        public JsonResult List(string order)
        {
            Scores lst = populateScores(order);
            if (order == "DESC")
                TempData["sortOrder"] = "ASC";
            else
                TempData["sortOrder"] = "DESC";

            return Json(lst);
        }



        //-- Populate list of scores, default order = "descending"
        public Scores populateScores(string order = "DESC")
        {
            DL oDL = new DL();
            Scores lst = oDL.GetScores(order);
            return lst;
        }



        //-- Async method to save score into the database
        public async Task<ActionResult> addScore(string player, int score)
        {
            DL oDL = new DL();
            Score s = new Score(0, player, score);
            Scores sc = null;
            bool bOk = false;
            try
            {
                bOk = await oDL.AddScore(s);
                sc = populateScores(TempData["sortOrder"].ToString());
                return View("List", sc);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}