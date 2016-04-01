//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;

//namespace WebSite.Controllers
//{
//    public class ErrorController : HWBaseController
//    {
//        protected override void HandleUnknownAction(string actionName)
//        {
//            return;
//            //base.HandleUnknownAction(actionName);
//        }

//        public class NotFoundViewModel
//        {
//            public string RequestedUrl { get; set; }
//            public string ReferrerUrl { get; set; }
//        }

//        public ActionResult Http404(String url)
//        {
//            Response.StatusCode     = (int)HttpStatusCode.NotFound;
//            NotFoundViewModel model = new NotFoundViewModel();
//            if (!String.IsNullOrEmpty(url))
//            {
//                // Если путь относительный ('NotFound' route), тогда его нужно заменить на запрошенный путь
//                model.RequestedUrl = Request.Url.OriginalString.Contains(url) & Request.Url.OriginalString != url ? Request.Url.OriginalString : url;
//                // Предотвращаем зацикливание при равенстве Referrer и Request
//                model.ReferrerUrl = Request.UrlReferrer != null &&
//                    Request.UrlReferrer.OriginalString != model.RequestedUrl ?
//                    Request.UrlReferrer.OriginalString : null;
//            }
//            // TODO: добавить реализацию ILogger
//            return View("NotFound", model);
//        }

//    }
//}
