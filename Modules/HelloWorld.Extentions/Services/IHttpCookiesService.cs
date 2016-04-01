using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace WebSite.Services
{
    public interface IHttpCookiesService
    {
        HttpCookie CreateCookie(string userName, bool isPersistent = false);
        HttpCookie GetAuthCookieFromContext();
        FormsAuthenticationTicket GetAuthTicketFromContext();
    }
}
