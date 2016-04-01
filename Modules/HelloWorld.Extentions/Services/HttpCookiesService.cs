using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace WebSite.Services
{
    public class HttpCookiesService:IHttpCookiesService
    {
        private HttpContext _httpContext;

        public HttpCookiesService(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _httpContext = context;
        }

        public HttpCookie CreateCookie(string userName, bool isPersistent = false)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
               1,
               userName,
               DateTime.Now,
               DateTime.Now.Add(FormsAuthentication.Timeout),
               isPersistent,
               String.Empty,
               FormsAuthentication.FormsCookiePath);

            String encTicket = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };

            _httpContext.Response.Cookies.Set(cookie);

            return cookie;
        }

        public System.Web.Security.FormsAuthenticationTicket GetAuthTicketFromContext()
        {
            HttpCookie authCookie = this.GetAuthCookieFromContext();
            if (authCookie != null && !String.IsNullOrEmpty(authCookie.Value))
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                return ticket;
            }
            else
            {
                return null;
            }
        }

        public HttpCookie GetAuthCookieFromContext()
        {
            return _httpContext.Request.Cookies.Get(FormsAuthentication.FormsCookieName); 
        }
    }
}