using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Common.Models.ViewModels
{
    public class CourseGroupVM
    {
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
        public bool IsNew { get; set; }

        public IEnumerable<CourseVM> Courses { get; set; }

        public int MinCoursePrice
        {
            get
            {
                int result = Int32.MaxValue;
                foreach (CourseVM course in this.Courses)
                {
                    foreach (CoursePriceVM p in course.Prices)
                    {
                        if (p.Price != 0 && p.Price < result)
                            result = (int)p.Price;
                    }
                    //if (course.PricePerMonth != 0 && course.PricePerMonth < result)
                    //    result = course.PricePerMonth;
                }
                return result == Int32.MaxValue ? 0 : result;
            }
        }
    
    }

    public class EditGroupVM:CreateGroupVM
    {
        [Required]
        public int GroupId { get; set; }
    }

    public class CreateGroupVM
    {
        [Required]
        public String GroupName { get; set; }
        [Required]
        public String BreadCrumb { get; set; }
        [Required]
        public String RouteName { get; set; }
        [Required]
        public int Order        { get; set; }
     
        public String ImagePath { get; set; }
        public String GoalsMarkup { get; set; }
        public String MethodMarkup { get; set; }
        public String SolutionsMarkup { get; set; }

        public String CustomPageHtml { get; set; }
        public String MenuItemStyle { get; set; }
        public String IsNew { get; set; }
    }

    public class EditPageHeadersVM
    {
        public int? GroupId  { get; set; }
        public int? CourseId { get; set; }

        public String TitleH1 { get; set; }
        public String Description { get; set; }
        public String KeyWords { get; set; }
        public String Robots { get; set; }
        public String PageTitle { get; set; }
    }
}
