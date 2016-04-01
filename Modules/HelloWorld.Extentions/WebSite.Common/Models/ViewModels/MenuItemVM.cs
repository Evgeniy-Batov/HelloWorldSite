using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class MenuItemVM
    {
        public String Text { get; set; }
        public String Controller { get; set; }
        public String Action { get; set; }
        public bool   Selected { get; set; }
        public String CssClassName { get; set; }
        public String ExternalLink { get; set; }

        public bool IsExternal
        {
            get { return !String.IsNullOrEmpty(this.ExternalLink); }
        }

        public ICollection<MenuItemVM> SubMenu { get; private set; }

        public MenuItemVM()
        {
            this.SubMenu = new List<MenuItemVM>();
        }
    }
}