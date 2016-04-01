using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Models.BusinessModels;

namespace WebSite.Repositories
{
    public interface IUserRepository
    {
        User GetUserByLogin(String login);
        User GetUserByLoginAndPassword(String login, String password);
    }
}
