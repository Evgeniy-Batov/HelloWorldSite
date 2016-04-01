using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using WebSite.Repositories;

namespace WebSite.Models.BusinessModels
{
    public class User
    {
        public static User SuperUser
        {
            get
            {
                User result  = new User();
                result.Email = "evgeniy.batov@gmail.com";
                return result;
            }
        }

        public static User SuperUser2
        {
            get
            {
                User result = new User();
                result.Email = "ann.barantsevich@mail.ru";
                return result;
            }
        }

        public String Email { get; private set; }

        public bool InRoles(String roles)
        {
            return false;
        }
    }

    public interface IUserProvider
    {
        User User { get; }
    }

    public class UserIdentity : IIdentity,IUserProvider
    {
        public User User { get; private set; }

        public string AuthenticationType
        {
            get { return typeof(User).ToString(); }
        }

        public bool IsAuthenticated
        {
            get { return this.User != null; }
        }

        public void Init(String email, IUserRepository repository)
        {
            if (!String.IsNullOrEmpty(email))
            {
                this.User = repository.GetUserByLogin(email);
            }
        }

        public string Name
        {
            get
            {
                if (this.User != null)
                {
                    return this.User.Email;
                }
                return "Anonymous";
            }
        }
    }

    public class UserPrincipal : IPrincipal
    {
        private UserIdentity _userIdentity { get; set; }

        public UserPrincipal(String name, IUserRepository repository)
        {
            _userIdentity = new UserIdentity();
            _userIdentity.Init(name, repository);
        }

        public override string ToString()
        {
            return _userIdentity.Name;
        }

        public IIdentity Identity
        {
            get { return _userIdentity; }
        }

        public bool IsInRole(string role)
        {
            if (_userIdentity.User == null)
            {
                return false;
            }
            return _userIdentity.User.InRoles(role);
        }
    }
}