using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Common.Models.ViewModels
{
    public enum ScheduledPaymentStatus
    {
        [Display(Name = "Неизвестен")]
        Unknown     = 0,
        [Display(Name = "Запланирован")]
        Scheduled   = 1,
        [Display(Name = "Оплачен")]
        Paid        = 2,
        [Display(Name = "Просрочка")]
        Posponed    = 3,
        [Display(Name = "Перенесён")]
        Rescheduled = 4,
        [Display(Name = "Отменён")]
        Canceled    = 5
    }

    public enum ScheduledPaymentRecurrent
    {
        [Display(Name   = "Неизвестен")]
        Unknown         = 0,
        [Display(Name   = "Ежемесячно фиксированный")]
        Monthly         = 1,
        [Display(Name   = "Ежемесячно почасовка")]
        MonthlyHours    = 2,
        [Display(Name   = "Ежемесячно по студентам")]
        MonthlyStudents = 3,
    }

    public class ScheduledPaymentVM
    {
        public int PaymentId                          { get; set; }
        
        public int NumberOfSentReminders              { get; set; }

        [Display(Name="Дата платежа")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime DueDate                       { get; set; }


        public String DueDateStr
        {
            get { return this.DueDate.ToShortDateString(); }
        }


        [Display(Name = "Сумма платежа")]
        public int ScheduledAmount { get; set; }

        [Display(Name = "Сумма платежа")]
        [Required]
        public decimal ScheduledAmountInHryvnas
        {
            get { return this.ScheduledAmount / 100.00m; }
            set { this.ScheduledAmount = (int)value * 100; }
        }

        public String StudentName { get; set; }

        public int StudentId { get; set; }

        public CourseFlowVM Flow { get; set; }

        [Display(Name = "Статус платежа")]
        [Required]
        public ScheduledPaymentStatus Status          { get; set; }

        [Display(Name = "Регулярный платёж?")]
        [Required]
        public bool IsReccurent { get; set; }

        [Display(Name = "Тип регулярности")]
        [Required]
        public ScheduledPaymentRecurrent RecurrentType { get; set; }

        [Display(Name = "Номер для недели (месяца)")]
        [Required]
        public int RecurrentMoment { get; set; }

        [Display(Name = "Расход")]
        [Required]
        public bool IsExpense { get; set; }

        [Display(Name = "Комментарий")]
        public String Comment { get; set; }

        public ScheduledPaymentVM GenerateNextMonth()
        {
            if (this.Status == ScheduledPaymentStatus.Canceled || this.Status == ScheduledPaymentStatus.Unknown)
                return null;

            ScheduledPaymentVM result = new ScheduledPaymentVM();
            result.Comment = this.Comment;
            result.DueDate = this.DueDate.AddMonths(1);
            result.Flow = this.Flow;
            result.IsExpense = this.IsExpense;
            result.IsReccurent = this.IsReccurent;
            result.RecurrentMoment = this.RecurrentMoment;
            result.RecurrentType = this.RecurrentType;
            result.ScheduledAmount = this.ScheduledAmount;
            result.Status = ScheduledPaymentStatus.Scheduled;
            result.StudentId = this.StudentId;
            result.StudentName = this.StudentName;

            return result;
        }

    }
}
