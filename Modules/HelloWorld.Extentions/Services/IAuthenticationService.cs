using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebSite.Models.BusinessModels;

namespace WebSite.Services
{
    public interface IAuthenticationService
    {
        User Login(String login, String password, bool isPersistent);

        void Logout();

        IPrincipal CurrentUser { get; }
    }
}
 