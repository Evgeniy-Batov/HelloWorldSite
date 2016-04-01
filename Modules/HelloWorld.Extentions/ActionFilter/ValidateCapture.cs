//using Recaptcha;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http.Formatting;
//using System.Web;
//using System.Web.Mvc;
//using WebSite.Common.Models.ViewModels;

//namespace WebSite.ActionFilter
//{
//    public class PreValidateCaptureAttribute : ActionFilterAttribute
//    {
//        private const string CHALLENGE_FIELD_KEY = "recaptcha_challenge_field";
//        private const string RESPONSE_FIELD_KEY = "recaptcha_response_field";

//        private RecaptchaResponse recaptchaResponse;

//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            if (!filterContext.ActionParameters.ContainsKey("message"))
//            {
//                filterContext.ActionParameters["captchaValid"] = false;
//                filterContext.ActionParameters["captchaErrorMessage"] = "Введён неверный код подтверждения";
//                base.OnActionExecuting(filterContext);
//                return;
//            }



//            OfflineMessage mgs = filterContext.ActionParameters["message"] as OfflineMessage;
//            if (mgs != null && mgs.recaptcha_challenge_field != null)
//            {
//                RecaptchaValidator recaptchaValidator = new RecaptchaValidator();
//                recaptchaValidator.PrivateKey = RecaptchaControlMvc.PrivateKey;
//                recaptchaValidator.RemoteIP = filterContext.HttpContext.Request.UserHostAddress;
//                recaptchaValidator.Challenge = mgs.recaptcha_challenge_field;
//                recaptchaValidator.Response = mgs.recaptcha_response_field;
//                this.recaptchaResponse = !string.IsNullOrEmpty(recaptchaValidator.Challenge) ? (!string.IsNullOrEmpty(recaptchaValidator.Response) ? recaptchaValidator.Validate() : RecaptchaResponse.InvalidResponse) : RecaptchaResponse.InvalidChallenge;
//                filterContext.ActionParameters["captchaValid"] = (object)(bool)(this.recaptchaResponse.IsValid);
//                filterContext.ActionParameters["captchaErrorMessage"] = (object)this.recaptchaResponse.ErrorMessage;
//                base.OnActionExecuting(filterContext);
//            }
//            else if (!String.IsNullOrEmpty(filterContext.HttpContext.Request[RESPONSE_FIELD_KEY]))
//            {
//                RecaptchaValidator recaptchaValidator = new RecaptchaValidator();
//                recaptchaValidator.PrivateKey = RecaptchaControlMvc.PrivateKey;
//                recaptchaValidator.RemoteIP = filterContext.HttpContext.Request.UserHostAddress;
//                recaptchaValidator.Challenge = filterContext.HttpContext.Request[CHALLENGE_FIELD_KEY];
//                recaptchaValidator.Response = filterContext.HttpContext.Request[RESPONSE_FIELD_KEY];
//                this.recaptchaResponse = !string.IsNullOrEmpty(recaptchaValidator.Challenge) ? (!string.IsNullOrEmpty(recaptchaValidator.Response) ? recaptchaValidator.Validate() : RecaptchaResponse.InvalidResponse) : RecaptchaResponse.InvalidChallenge;
//                filterContext.ActionParameters["captchaValid"] = (object)(bool)(this.recaptchaResponse.IsValid);
//                filterContext.ActionParameters["captchaErrorMessage"] = (object)this.recaptchaResponse.ErrorMessage;
//                base.OnActionExecuting(filterContext);
//            }
//            else
//            {
//                throw new ArgumentOutOfRangeException();
//            }
          
//        }
//    }
//}