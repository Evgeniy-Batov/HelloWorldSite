using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebSite.Common.Models.ViewModels;

namespace WebSite.ViewModels
{
    public enum ReminderType
    {
        [Display(Name = "Email")]
        Email,
        [Display(Name = "SMS")]
        SMS,
        [Display(Name = "Email+SMS")]
        SMSAndEmail
    }

    public class ReminderBindingModel
    {
        public ReminderBindingModel()
        {

        }

        public ReminderBindingModel(int paymentId, EmailVM emailVm)
        {
            this.PaymentId = paymentId;
            this.Emails    = emailVm.RecipientsTo;
            this.Phones    = emailVm.RecipientPhones;
            this.BCCs      = emailVm.BccTo;
            this.Subject   = emailVm.Subject;
            this.Body      = emailVm.Body;
        }

        [Required]
        public int PaymentId { get; set; }

        public String Emails { get; set; }

        public String Phones { get; set; }

        public String BCCs { get; set; }

        [Required]
        public String Subject { get; set; }

        [Required]
        public String Body { get; set; }

        public ReminderType Type { get; set; }
    }
}