using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Repositories
{
    public class UserRepository:IUserRepository
    {
        public Models.BusinessModels.User GetUserByLogin(string login)
        {
            if (login == Models.BusinessModels.User.SuperUser.Email)
            {
                return Models.BusinessModels.User.SuperUser;
            }
            else if (login == Models.BusinessModels.User.SuperUser2.Email)
            {
                return Models.BusinessModels.User.SuperUser2;
            }
            return null;
        }

        public Models.BusinessModels.User GetUserByLoginAndPassword(string login, string password)
        {
            if (login == "evgeniy.batov@gmail.com" && password == "ajhvbhjdfybt123")
            {
                return Models.BusinessModels.User.SuperUser;
            }
            else if (login == "ann.barantsevich@mail.ru" && password == "24031983")
            {
                return Models.BusinessModels.User.SuperUser2;
            }
            return null;
        }
    }
}