using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Models.ViewModels;

namespace WebSite.DAL.Db.Models
{
    public class CourseGroupDbM
    {
        public virtual ICollection<CourseDbM> Courses { get; set; }

        public int GroupId { get; set; }
        public String GroupName { get; set; }
        public String BreadCrumb { get; set; }
        public String ImagePath { get; set; }
        public String GoalsMarkup { get; set; }
        public String MethodMarkup { get; set; }
        public String SolutionsMarkup { get; set; }
        public String RouteName { get; set; }
        public int Order { get; set; }

        public String TitleH1 { get; set; }
        public String Description { get; set; }
        public String KeyWords { get; set; }
        public String Robots { get; set; }
        public String PageTitle { get; set; }
        public String CustomPageHtml { get; set; }
        public String MenuItemStyle { get; set; }
        public bool? IsNew { get; set; }


        public void ModifyHeaders(EditPageHeadersVM headers)
        {
            this.TitleH1 = headers.TitleH1;
            this.Description = headers.Description;
            this.KeyWords = headers.KeyWords;
            this.Robots = headers.Robots;
            this.PageTitle = headers.PageTitle;
        }

        public void Modify(EditGroupVM changes)
        {
            this.GroupName = changes.GroupName;
            this.BreadCrumb = changes.BreadCrumb;
            this.ImagePath = changes.ImagePath;
            this.GoalsMarkup = changes.GoalsMarkup;
            this.MethodMarkup = changes.MethodMarkup;
            this.SolutionsMarkup = changes.SolutionsMarkup;
            this.RouteName = changes.RouteName;
            this.Order = changes.Order;

            this.CustomPageHtml = changes.CustomPageHtml;
            this.MenuItemStyle = changes.MenuItemStyle;
            this.IsNew = changes.IsNew == "on";
        }
    }
}
