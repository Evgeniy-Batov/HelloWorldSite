using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebSite.Common.Models.ViewModels;

namespace WebSite.ViewModels
{
    public class RegisterStudentVM
    {
        //For model binder usage only
        public RegisterStudentVM()
        {

        }

        public RegisterStudentVM(CourseVM course, CourseFlowVM flow, IEnumerable<SignupApplication> registrants, IEnumerable<ScheduledPaymentVM> payments)
        {
            this.ScheduledPayments = payments;
            this.Applications      = registrants;
            this.CourseName        = course.CourseName;
            this.CourseId          = course.CourseId;

            this.FlowStartDate = flow.ActualStartDate.HasValue ? flow.ActualStartDate.Value : DateTime.Today ;
            this.FlowEndDate   = flow.ActualEndDate.HasValue   ? flow.ActualEndDate.Value   : DateTime.Today.AddMonths(1);

            this.FlowId        = flow.FlowId;
            this.StudentSince  = this.FlowStartDate;
            this.StudentTill   = this.FlowEndDate;
        }

        public int StudentId { get; set; }

        public String CourseName { get; set; }
        
        [Required]
        public int FlowId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public DateTime FlowStartDate { get;set; }

        [Required]
        public DateTime FlowEndDate { get; set; }

        public IEnumerable<SignupApplication> Applications { get; set; }

        public IEnumerable<ScheduledPaymentVM> ScheduledPayments { get; set; }

        [Required]
        [Display(Name ="Имя студента")]
        public String FirstName { get; set; }

        [Required]
        [Display(Name = "Отчество")]
        public String MiddleName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public String LastName { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        [Display(Name = "Телефон")]
        public String Phone1 { get; set; }

        [Display(Name = "Дополнительный телефон")]
        public String Phone2 { get; set; }

        [Display(Name = "Способ оплаты")]
        [Required]
        public StudentPaymentModel PaymentModel { get; set; }

        [Display(Name = "Номер паспорта")]
        public String PassportNo { get; set; }

        [Display(Name = "Дата выдачи паспорта")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PasspordIssuedDate { get; set; }

        [Display(Name = "Орган выдачи паспорта")]
        public String PassportIssuedBy { get; set; }

        [Display(Name = "Идентификационный код")]
        public String INN { get; set; }

        [Required]
        [Display(Name = "Номер договора")]
        public String ContractNumber { get; set; }

        [Display(Name = "Комментарий")]
        public String Comment { get; set; }
        
        [Display(Name = "Скидка 'приведи друга'")]
        public bool FriendBonus { get; set; }

        [Display(Name = "Скидка 'лояльный клиент'")]
        public bool SecondCourseBonus { get; set; }

        [Required]
        public DateTime StudentSince { get; set; }

        [Required]
        public DateTime StudentTill { get; set; }
    }
}