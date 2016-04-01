using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Utils;

namespace WebSite.Common.Models.ViewModels
{
    public class EditCourseInfoVM
    {
        [Required]
        public int CourseId { get; set; }
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

    public class CreateCourseVM
    {
        [Required]
        public String CourseName { get; set; }
        public int CourseGroupId { get; set; }
        public String Breadcrumb { get; set; }
        public String Route { get; set; }
        public int PricePerMonth { get; set; }
        public String CourseImageRef { get; set; }
        public String WhatIsItHtml { get; set; }
        public String WhoRequresIt { get; set; }
        public String WhatForHtml { get; set; }
        public String HowToAchieve { get; set; }
        public String WhatIsRequired { get; set; }
        public int Order { get; set; }

        public String NextCourse { get; set; }
        public String PreviousCourse { get; set; }

        public String CustomPageHtml { get; set; }
        public String MenuItemStyle { get; set; }
        public String IsNew { get; set; }
    }

    public class EditCourseVM : CreateCourseVM
    {
        public int CourseId { get; set; }
    }

    public class CoursePriceVM
    {
        public int PriceId { get; set; }
        public int CourseId { get; set; }
        public Decimal Price { get; set; }
        public String Condition { get; set; }
        public String CustomCSS { get; set; }
        public DateTime? ValidTill { get; set; }
        public String ShortCondition { get; set; }
   }

    public class UpdateCourseVM : CoursePriceVM
    {
        public String id { get; set; }
        public String oper { get; set; }
    }

    public class CourseVM
    {
        public int CourseId { get; set; }
        public String CourseName { get; set; }
        public int CourseGroupId { get; set; }
        public String CourseGroupName { get; set; }

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

        public CourseGroupVM Group { get; set; }
        public EditCourseInfoVM Info { get; set; }

        public IEnumerable<CoursePriceVM> Prices { get; set; }

        public String CustomPageHtml { get; set; }
        public String MenuItemStyle { get; set; }
        public bool IsNew { get; set; }


        public int CourseLengthInMonth(int lessonsPerWeek)
        {
            int numberOfClasses = this.PricePerMonth;

            float numberOfWeeks = (float)numberOfClasses / lessonsPerWeek;

            float numberOfDays = numberOfWeeks * 7;

            return (int)Math.Round(numberOfDays / 30.0f);
        }

    }

    public class CourseFlowVM
    {
        public int NumberOfLessonsInWeek
        {
            get
            {
                int result = 0;

                if (this.MondayEnd != this.MondayStart)
                    result++;
                if (this.ThuesdayEnd != this.ThuesdayStart)
                    result++;
                if (this.WednesdayEnd != this.WednesdayStart)
                    result++;
                if (this.ThursdayEnd != this.ThursdayStart)
                    result++;
                if (this.FridayEnd != this.FridayStart)
                    result++;
                if (this.SaturdayEnd != this.SaturdayStart)
                    result++;
                if (this.SundayEnd != this.SundayStart)
                    result++;

                return result;
            }
        }


        public String MondayStart { get; set; }
        public String MondayEnd { get; set; }
        public String ThuesdayStart { get; set; }
        public String ThuesdayEnd { get; set; }
        public String WednesdayStart { get; set; }
        public String WednesdayEnd { get; set; }
        public String ThursdayStart { get; set; }
        public String ThursdayEnd { get; set; }
        public String FridayStart { get; set; }
        public String FridayEnd { get; set; }
        public String SaturdayStart { get; set; }
        public String SaturdayEnd { get; set; }
        public String SundayStart { get; set; }
        public String SundayEnd { get; set; }


        public int FlowId { get; set; }

        [Display(Name ="Курс")]
        public int CourseId { get; set; }

        [Display(Name = "Планируемая дата начала занятий")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EstimatedStartDate { get; set; }

        [Display(Name = "Название потока")]
        public String CustomName { get; set; }

        [Display(Name = "Фактическая дата начала занятий")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ActualStartDate { get; set; }
        
        [Display(Name = "Статус потока")]
        public FlowStatus Status { get; set; }

        public int StatusId
        {
            get { return (int)this.Status; }
        }

        [Display(Name = "Фактическая дата окончания занятий")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ActualEndDate { get; set; }

        [Display(Name = "Описание даты начала")]
        public String StartDateStr { get; set; }
    
        public String StatusStr
        {
            get { return EnumHelper<FlowStatus>.GetDisplayValue(this.Status); }
        }


        public static CourseFlowVM CreateDefault(int courseId)
        {
            CourseFlowVM flow = new CourseFlowVM();
            flow.CourseId     = courseId;
            flow.CustomName   = "Ближайший набор";
            flow.StartDateStr = "Нет точной даты (по мере набора группы)";
            flow.Status       = FlowStatus.OpenedForRegistration;
            return flow;
        }
    }

    public enum FlowStatus
    {
        [Display(Name = "Регистрация")]
        OpenedForRegistration = 0,
        [Display(Name = "Обучение")]
        Started = 1,
        [Display(Name = "Курс Окончен")]
        Finished = 2,
        [Display(Name ="Отменён")]
        Canceled = 3
    }
}
