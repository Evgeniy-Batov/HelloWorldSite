using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Models.ViewModels;
using WebSite.DAL.Db.Models.Models;

namespace WebSite.DAL.Db.Models
{
    public class CourseDbM
    {
        #region Construction

        public CourseDbM()
        {
        }

        public void ModifyHeaders(EditPageHeadersVM headers)
        {
            this.TitleH1 = headers.TitleH1;
            this.Description = headers.Description;
            this.KeyWords = headers.KeyWords;
            this.Robots = headers.Robots;
            this.PageTitle = headers.PageTitle;
        }


        public void ApplyChanges(CreateCourseVM createCourse)
        {
            this.CourseName = createCourse.CourseName;
            this.CourseGroupId = createCourse.CourseGroupId;
            this.Breadcrumb = createCourse.Breadcrumb;
            this.Route = createCourse.Route;
            this.PricePerMonth = createCourse.PricePerMonth;
            this.CourseImageRef = createCourse.CourseImageRef;
            this.WhatIsItHtml = createCourse.WhatIsItHtml;
            this.WhoRequresIt = createCourse.WhoRequresIt;
            this.WhatForHtml = createCourse.WhatForHtml;
            this.HowToAchieve = createCourse.HowToAchieve;
            this.WhatIsRequired = createCourse.WhatIsRequired;
            this.Order = createCourse.Order;
            this.NextCourse = createCourse.NextCourse;
            this.PreviousCourse = createCourse.PreviousCourse;

            this.CustomPageHtml = createCourse.CustomPageHtml;
            this.MenuItemStyle  = createCourse.MenuItemStyle;
            this.IsNew          =  (createCourse.IsNew  != null && createCourse.IsNew == "on");
        }

        public void AddPrice(CoursePriceVM newPrice)
        {
            CoursePriceDbM p = new CoursePriceDbM();
            p.Conditional = newPrice.Condition;
            p.Course = this;
            p.CourseId = this.CourseId;
            p.CustomCSS = newPrice.CustomCSS;
            p.Price = newPrice.Price;
            p.ShortConditional = newPrice.ShortCondition;
            p.ValidTill = newPrice.ValidTill;

            this.AdditionalPrices.Add(p);
        }

        public void UpdatePrice(CoursePriceVM updatedPrice)
        {
            CoursePriceDbM currentPrice = this.AdditionalPrices.FirstOrDefault(p => p.PriceId.Equals(updatedPrice.PriceId));

            if (currentPrice != null)
            {
                currentPrice.Conditional = updatedPrice.Condition;
                currentPrice.CustomCSS = updatedPrice.CustomCSS;
                currentPrice.Price = updatedPrice.Price;
                currentPrice.ShortConditional = updatedPrice.ShortCondition;
                currentPrice.ValidTill = updatedPrice.ValidTill;
            }
        }

        public void RemovePrice(int id)
        {
            CoursePriceDbM currentPrice = this.AdditionalPrices.FirstOrDefault(p => p.PriceId.Equals(id));

            if (currentPrice != null)
            {
                this.AdditionalPrices.Remove(currentPrice);
            }
        }



        public CourseDbM(CreateCourseVM createCourse)
            :this()
        {
            this.ApplyChanges(createCourse);
        }

        #endregion

        public int CourseId { get; set; }
        public int CourseGroupId { get; set; }
        public String CourseName { get; set; }

        public virtual CourseGroupDbM Group { get; set; }
        public virtual CourseInfoDbM CourseInfo { get; set; }
        public virtual ICollection<FlowDbModel> Flows { get; set; }
        public virtual ICollection<CoursePriceDbM> AdditionalPrices { get; set; }

        public String Breadcrumb {get;set;}
        public String Route { get; set; }
        public int PricePerMonth { get; set; }
        public String CourseImageRef { get; set; }
        public String WhatIsItHtml { get; set; }
        public String WhoRequresIt { get; set; }
        public String WhatForHtml {get;set;}
        public String HowToAchieve { get; set; }
        public String WhatIsRequired { get; set; }
        public int Order { get; set; }

        public String TitleH1 { get; set; }
        public String Description { get; set; }
        public String KeyWords { get; set; }
        public String Robots { get; set; }
        public String PageTitle { get; set; }

        public String NextCourse { get; set; }
        public String PreviousCourse { get; set; }
        public String CustomPageHtml { get; set; }
        public String MenuItemStyle { get; set; }
        public bool? IsNew { get; set; }
    }
}
