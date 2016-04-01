using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Введите email")]
        public String Email    { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public String Password { get; set; }

        public bool IsPersistent { get; set; }

        public String ReturnUrl { get; set; }
    }
}