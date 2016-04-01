using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Common.Models.ViewModels
{
    public class MainPageItemVM
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public String DestinationAction { get; set; }
        public String DestinationActionParams { get; set; }
        public String DestinationController { get; set; }
        public String ImgUrl { get; set; }
        public String ItemCss { get; set; }
        public String ItemText { get; set; }
        public String ItemTitle { get; set; }
    }
}
