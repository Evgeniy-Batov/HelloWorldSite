using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Models.ViewModels;

namespace WebSite.DAL.Db.Models
{
    public class MainPageItemDbM
    {
        public MainPageItemDbM()
        {
        }

        public MainPageItemDbM(MainPageItemVM model)
            :this()
        {
            this.ApplyChanges(model);
        }



        public int Id { get; set; }
        public int Order { get; set; }
        
        public String DestinationAction { get; set; }
        public String DestinationActionParams { get; set; }
        public String DestinationController { get; set; }
        public String ImgUrl { get; set; }
        public String ItemCss { get; set; }
        public String ItemText { get; set; }
        public String ItemTitle { get; set; }

        public void ApplyChanges(MainPageItemVM model)
        {
            this.DestinationAction = model.DestinationAction;
            this.DestinationActionParams = model.DestinationActionParams;
            this.DestinationController = model.DestinationController;
            this.Id = model.Id;
            this.ImgUrl = model.ImgUrl;
            this.ItemCss = model.ItemCss;
            this.ItemText = model.ItemText;
            this.ItemTitle = model.ItemTitle;
            this.Order = model.Order;
        }
    }
}
