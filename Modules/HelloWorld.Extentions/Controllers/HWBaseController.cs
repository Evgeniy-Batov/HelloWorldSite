//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Routing;
//using WebSite.Models.BusinessModels;
//using WebSite.Repositories;
//using WebSite.Services;

//namespace WebSite.Controllers
//{
//    public class HWBaseController : Controller
//    {
//        public HWBaseController(IAuthenticationService authService)
//        {
//            //if (authService == null)
//            //    throw new ArgumentNullException("authService");

//            //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
//            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru-RU");

//            this.AuthenticationService = authService;
//        }

//        //TODO:ADD HONEST DI HERE
//        public HWBaseController()
//            :this(new AuthenticationService(new HttpCookiesService(System.Web.HttpContext.Current), new UserRepository()))
//        {
//        }

//        public IAuthenticationService AuthenticationService { get; private set; }

//        public User CurrentUser
//        {
//            get
//            {
//                return ((IUserProvider)this.AuthenticationService.CurrentUser.Identity).User;
//            }
//        }

//        protected bool IsViewAvailable(String viewName)
//        {
//            ControllerContext controllerContext = this.ControllerContext;
//            ViewEngineResult result = ViewEngines.Engines.FindView(controllerContext, viewName, null);
//            return result.View != null;
//        }

//        protected override void OnException(ExceptionContext filterContext)
//        {
//            if (filterContext != null && filterContext.Exception != null)
//            {
//                HttpException exception = filterContext.Exception as HttpException;
//                if (exception != null && exception.GetHttpCode() == 404)
//                {
//                    InvokeHttp404(filterContext.HttpContext);
//                    filterContext.ExceptionHandled = true;
//                    return;
//                }
//            }

//            base.OnException(filterContext);
//        }

//        protected override void HandleUnknownAction(string actionName)
//        {
//            this.InvokeHttp404(HttpContext);
//        }

//        public ActionResult InvokeHttp404(HttpContextBase httpContext)
//        {
//            IController errorController = new ErrorController();
//            RouteData errorRoute = new RouteData();
//            errorRoute.Values.Add("controller", "Error");
//            errorRoute.Values.Add("action", "Http404");
//            errorRoute.Values.Add("url", httpContext.Request.Url.OriginalString);
//            errorController.Execute(new RequestContext(
//                 httpContext, errorRoute));

//            return new EmptyResult();
//        }

//    }
//}
