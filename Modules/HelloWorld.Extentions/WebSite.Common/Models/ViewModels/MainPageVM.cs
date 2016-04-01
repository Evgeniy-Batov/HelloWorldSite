using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Common.Models.ViewModels;

namespace WebSite.Models
{
    public class MainPageVM
    {
        private ICollection<MainPageItemVM> _items;

        public MainPageVM()
        {
            _items = new List<MainPageItemVM>();
        }

        public class MainPageItemVM
        {
            public String ImgUrl {get;set;}
            public String ItemTitle { get; set; }
            public String ItemText { get; set; }
            public String ItemCss { get; set; }
            public String  DestinationAction { get; set; }
            public String  DestinationController { get; set; }
            public String  DestinationActionParams { get; set; }
        }

        public void AddItem(MainPageItemVM item)
        {
            if (null == item)
            {
                throw new ArgumentNullException("item");
            }
            _items.Add(item);
        }

        public String InfoText { get; set; }
        public IEnumerable<MainPageItemVM> PageItems 
        { 
            get 
            {
                return _items;
            }
        }

        public PageLayoutVM Layout { get; set; } 
    }
}