using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Models.ViewModels;

namespace WebSite.DAL.Db.Models
{
    public class PageLayoutDbM
    {
        public PageLayoutDbM()
        {

        }

        public PageLayoutDbM(PageLayoutVM vm)
            :this()
        {
            this.ApplyChanges(vm);
        }


        public void ApplyChanges(PageLayoutVM vm)
        {
            this.Description = vm.Description;
            this.Id = vm.Id;
            this.KeyWords = vm.KeyWords;
            this.Markup = vm.Markup;
            this.PageName = vm.PageName;
            this.PageTitle = vm.PageTitle;
            this.Robots = vm.Robots;
            this.TitleH1 = vm.TitleH1;
        }


        public int Id { get; set; }
        public String PageName { get; set; }
        public String PageTitle { get; set; }
        public String TitleH1 { get; set; }
        public String Description { get; set; }
        public String KeyWords { get; set; }
        public String Robots { get; set; }
        public String Markup { get; set; }
    }
}
