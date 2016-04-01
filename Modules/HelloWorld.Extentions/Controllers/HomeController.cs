using HelloWorld.Extentions.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld.Extentions.Controllers
{
    public class HomeController: Controller
    {

		[HttpGet]
		public ActionResult Index()
        {
            return View("IncomingMessages");
        }

		[HttpPost]
		public ActionResult Register(RegistrationVM registration)
		{
			return new ContentResult();
		}

		[HttpPost]
		public ActionResult LeaveQuestion(LeaveQuestionVM question)
		{
			return new ContentResult();
		}
    }
}