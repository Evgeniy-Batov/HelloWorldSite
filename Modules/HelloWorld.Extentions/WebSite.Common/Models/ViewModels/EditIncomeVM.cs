using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Common.Models.ViewModels
{
    public enum IncomeType
    {
        [Display(Name = "Другое")]
        Other          = 0,
        [Display(Name = "Платежи студентов")]
        StudentPayment = 1
    }

    public enum ExpenseType
    {
        [Display(Name = "Другое")]
        Other = 0,
        [Display(Name = "Аренда")]
        Rent = 10,
        [Display(Name = "Канцтовары")]
        Stationery = 11,
        [Display(Name = "Оборудование")]
        Equipment = 12,
        [Display(Name = "Зарплата")]
        Salary = 13,
        [Display(Name = "Возврат денег")]
        MoneyBack = 14
    }


    public class EditExpenseVM
        :EditIncomeVM
    {
        public ExpenseType ExpenseType { get; set; }
    }

    public class EditIncomeVM
    {
        public EditIncomeVM()
        {
            this.PaymentDate = DateTime.Today;
            this.Amount      = 0;
            this.PaymentType = IncomeType.Other;
            this.Courses = new List<CourseVM>();
        }

        [Display(Name = "Курсы")]
        public List<CourseVM> Courses { get; set; }

        public int? SelectedCourseId { get; set; }

        public int? SelectedFlowId { get; set; }

        public int? SelectedStudentId { get; set; }

        public int? ScheduledPaymentId { get; set; }

        [Display(Name ="Сумма")]
        [Required]
        public decimal     Amount             { get; set; }


        public String AmountDisplay
        {
            get { return string.Format("{0} грн.", this.Amount / 100.00m); }
        }


        [Display(Name = "Дата платежа")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime PaymentDate        { get; set; }

        public String   RegisteredBy       { get; set;}

        [Display(Name = "Комментарий")]
        public String   Comment            { get; set; }

        [Display(Name = "Источник дохода")]
        public IncomeType PaymentType      { get; set; }

        public int PaymentId { get; set; }

        public String StudentName { get; set; }

        public String FlowName { get; set; }

        public String CourseName { get; set; }

        public String PaymentName { get; set; }
        
    }
}
