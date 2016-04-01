using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Common.Models.ViewModels
{
    public enum StudentPaymentModel
    {
        [Display(Name = "Ежемесячные платежи")]
        Monthly = 1,
        [Display(Name = "Единый платёж")]
        SinglePayent = 2,
        [Display(Name = "Раз в 2 месяца")]
        By2Monthes = 3
    }


    public enum StudentStatus
    {
        [Display(Name = "Зачислен")]
        Active = 0,
        [Display(Name = "Отчислен")]
        Excluded = 1,
        [Display(Name = "Звкончил обучение")]
        Graduated = 2
    }

    public class StudentVM
    {
        public StudentVM()
        {
            this.Payments = new List<ScheduledPaymentVM>();
        }

        public int StudentId     { get; set; }

        public StudentStatus Status { get; set; }

        [Required]
        public int FlowId        { get; set; }

        [Required]
        [Display(Name = "Имя студента")]
        public String FirstName  { get; set; }

        [Required]
        [Display(Name = "Отчество")]
        public String MiddleName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public String LastName   { get; set; }

        [Required]
        public String Email      { get; set; }

        [Required]
        [Display(Name = "Телефон")]
        public String Phone1     { get; set; }

        [Display(Name = "Дополнительный телефон")]
        public String Phone2     { get; set; }

        [Display(Name = "Способ оплаты")]
        [Required]
        public StudentPaymentModel PaymentModel { get; set; }

        [Display(Name = "Номер паспорта")]
        public String   PassportNo         { get; set; }

        [Display(Name = "Дата выдачи паспорта")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PasspordIssuedDate { get; set; }

        
        public String   PassportIssuedAt   { get; set; }

        [Display(Name = "Орган выдачи паспорта")]
        public String   PassportIssuedBy   { get; set; }

        [Display(Name = "Идентификационный код")]
        public String   INN { get; set; }

        [Required]
        [Display(Name = "Номер договора")]
        public String  ContractNumber { get; set; }

        [Display(Name = "Комментарий")]
        public String  Comment { get; set; }

        public DateTime StudentSince { get; set; }
        public DateTime? StudentTill { get; set; }

        [Display(Name = "Имя студента")]
        public String FullName
        {
            get { return String.Format("{0} {1} {2}", this.LastName, this.FirstName, this.MiddleName); }
        }

        public List<ScheduledPaymentVM> Payments
        {
            get; set;
        }

    }
}
