using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using WebSite.Models.BusinessModels;
using WebSite.Repositories;

namespace WebSite.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private IPrincipal      _currentUser;
        private IUserRepository _repository;
        private IHttpCookiesService _httpCookies;

        public AuthenticationService(IHttpCookiesService cookies,IUserRepository repository)
        {
            if (cookies == null)
                throw new ArgumentNullException("cookies");

            if (repository == null)
                throw new ArgumentNullException("repository");

            _httpCookies = cookies;
            _repository  = repository;
        }

        public User Login(string login, string password, bool isPersistent)
        {
            User retUser = _repository.GetUserByLoginAndPassword(login, password);

            if (retUser != null)
            {
                _httpCookies.CreateCookie(login, isPersistent);
            }
            return retUser;
        }

        public void Logout()
        {
            HttpCookie cookie = _httpCookies.GetAuthCookieFromContext();
            if (cookie != null)
            {
                cookie.Value = null;
            }
        }

        public System.Security.Principal.IPrincipal CurrentUser
        {
            get 
            {
                if (_currentUser == null)
                {
                    try
                    {
                        FormsAuthenticationTicket  ticket = _httpCookies.GetAuthTicketFromContext();
                        if (ticket != null)
                        {
                            _currentUser = new UserPrincipal(ticket.Name, _repository);
                        }
                        else
                        {
                            _currentUser = new UserPrincipal(null, null);
                        }
                    }
                    catch(Exception)
                    {
                        //TODO:Add logging here
                        _currentUser = new UserPrincipal(null, null);
                    }
                }
                return _currentUser;
            }
        }
    }
}