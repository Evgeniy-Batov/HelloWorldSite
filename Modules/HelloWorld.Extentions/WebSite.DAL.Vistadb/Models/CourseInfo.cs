using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Models.ViewModels;
using WebSite.DAL.Db.Models.Models;

namespace WebSite.DAL.Db.Models
{
    public class CourseInfoDbM
    {
        public CourseInfoDbM()
        {
        }

        public CourseInfoDbM(EditCourseInfoVM vm)
            :this()
        {
            this.ApplyChanges(vm);
        }

        public void ApplyChanges(EditCourseInfoVM vm)
        {
            this.CourseId = vm.CourseId;
            this.ActionHtml = vm.ActionHtml;
            this.ExtraJS = vm.ExtraJS;
            this.LengthHtml = vm.LengthHtml;
            this.NewsHtml = vm.NewsHtml;
            this.PriceHtml = vm.PriceHtml;
            this.RenderAction = vm.RenderAction;
            this.RenderFB = vm.RenderFB;
            this.RenderLength = vm.RenderLength;
            this.RenderNews = vm.RenderNews;
            this.RenderOK = vm.RenderOK;
            this.RenderPrice = vm.RenderPrice;
            this.RenderSchedule = vm.RenderSchedule;
            this.RenderSignUp = vm.RenderSignUp;
            this.RenderSyllabus = vm.RenderSyllabus;
            this.RenderVK = vm.RenderVK;
            this.ScheduleHtml = vm.ScheduleHtml;
            this.SignUpHtml = vm.SignUpHtml;
            this.SyllabusHtml = vm.SyllabusHtml;
        }


        public int CourseId { get; set; }

        public virtual CourseDbM Course { get; set; }
        
        public bool RenderLength { get; set; }
        public String LengthHtml { get; set; }
        public bool RenderSchedule { get; set; }
        public String ScheduleHtml { get; set; }
        public bool RenderSyllabus { get; set; }
        public String SyllabusHtml { get; set; }
        public bool RenderPrice { get; set; }
        public String PriceHtml { get; set; }
        public bool RenderAction { get; set; }
        public String ActionHtml { get; set; }
        public bool RenderNews { get; set; }
        public String NewsHtml { get; set; }
        public bool RenderSignUp { get; set; }
        public String SignUpHtml { get; set; }
        public bool RenderVK { get; set; }
        public bool RenderOK { get; set; }
        public bool RenderFB { get; set; }
        public String ExtraJS { get; set; }
    }

    
}
