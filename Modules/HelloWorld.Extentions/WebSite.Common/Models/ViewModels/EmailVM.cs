using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Models.BusinessModels;

namespace WebSite.Common.Models.ViewModels
{
    public enum EmailStatus
    {
        [Display(Name = "В очереди")]
        Pending,
        [Display(Name = "Отправляется")]
        Sending,
        [Display(Name = "Отправлено")]
        Sent
    }

    public class EmailVM
    {
        public int EmailId { get; set; }
        public String From { get; set; }
        public String Subject { get; set; }
        public String Body { get; set; }
        public DateTime? SentTime { get; set; }
        public EmailStatus Status { get; set; }
        public bool IsHtml { get; set; }
        public ICollection<EmailRecipientVM> Recipients { get; set; }

        public String RecipientsTo
        {
            get { return String.Join(";", this.Recipients.Where(r => r.To).Select(r => r.Recepient)); }
        }

        public String RecipientPhones
        {
            get { return String.Join(";", this.Recipients.Where(r => r.Phone).Select(r => r.Recepient)); }
        }

        public String BccTo
        {
            get { return String.Join(";", this.Recipients.Where(r => r.BCC).Select(r => r.Recepient)); }
        }
    }

    public enum EmailTemplate
    {
        SignupOwner               = 1,
        SignupUser                = 2,
        ConfirmationUser          = 3,
        ConfirmationOwners        = 4,
        CancelationUser           = 5,
        CancelationOwners         = 6,
        UpdateUser                = 7,
        UpdateOwners              = 8,
        ReminderUser              = 9,
        ReminderOwners            = 13,
        OfflineQuestionUser       = 10,
        OfflineQuestionOwner      = 11,
        PaymentNotifiction        = 12
    }


    public class EmailTemplateVM
    {
        public int TemplateId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        private String GetSelectedTime(SignupApplication application, DayOfWeek day)
        {
            LessonsTime time = application.Schedule.GetDesiredTime(day);
            switch (time)
            {
                case LessonsTime.Morning:
                    return "Утро";
                case LessonsTime.Evening:
                    return "Вечер";
                case LessonsTime.Whatever:
                    return "Любое время";
                case LessonsTime.Unavailable:
                    return "Нет возможности";
                default:
                    break;
            }
            return String.Empty;
        }

        public EmailVM BuildEmail(Func<String, String> filePathResolver, OfflineMessage message, String recipient, params String[] hiddenCopies)
        {
            if (String.IsNullOrEmpty(this.Body))
                return null;

            String fileName = filePathResolver(this.Body);

            if (!File.Exists(fileName))
                return null;

            String template = File.ReadAllText(fileName);

            String emailText = template.Replace("@firstname",  message.Name)
           .Replace("@email", message.Email)
           .Replace("@phone", message.Phone)
           .Replace("@coursename", message.Topic)
           .Replace("@additionalinfo", message.Message);

            EmailVM result = new EmailVM();
            result.From = "info@kharkovitcourses.com";
            result.Recipients = new List<EmailRecipientVM>();
            result.SentTime = SmartTime.Now;
            result.Status = EmailStatus.Pending;
            result.Subject = this.Subject;
            result.Body = emailText;

            EmailRecipientVM to = new EmailRecipientVM();
            to.Recepient = recipient;
            to.To = true;

            result.Recipients.Add(to);

            foreach (String s in hiddenCopies)
            {
                EmailRecipientVM bcc = new EmailRecipientVM();
                bcc.Recepient = s;
                bcc.BCC = true;
                result.Recipients.Add(bcc);
            }
            return result;
        }

        public EmailVM BuildEmail(Func<String, String> filePathResolver, SignupApplication application, CourseVM course, CourseFlowVM flow,StudentVM student, String editLink, String confirmationLink, String cancelLink, String recipient, params String[] hiddenCopies)
        {
            String paymentAmount  = "0 грн";
            String paymentPeriod  = "";
            String paymnetDate    = DateTime.Today.ToShortDateString();
            String contractNumber = "";

            if (student != null && student.Payments != null)
            {
                contractNumber =  String.Format("№ \"{0}\"",student.ContractNumber);

                ScheduledPaymentVM payment = student.Payments.OrderBy(p => p.DueDate).Where(p => p.Status == ScheduledPaymentStatus.Scheduled).FirstOrDefault();
                if (payment != null)
                {
                    paymentAmount = String.Format("{0}грн.", payment.ScheduledAmount / 100.00m);
                    paymnetDate   = payment.DueDate.ToShortDateString();

                    if (payment.Comment != null && payment.Comment.Contains("за месяц"))
                    {
                        paymentPeriod = payment.Comment.Substring(payment.Comment.IndexOf("за месяц"));
                    }
                }
            }

            if (String.IsNullOrEmpty(this.Body))
                return null;

            String template;

            if (filePathResolver != null)
            {
                String fileName = filePathResolver(this.Body);

                if (!File.Exists(fileName))
                    return null;

                template = File.ReadAllText(fileName);
            }
            else
            {
                template = this.Body;
            }

            String startTime = flow.EstimatedStartDate.HasValue ? flow.EstimatedStartDate.Value.ToShortDateString() : flow.StartDateStr;

            String emailText = template.Replace("@firstname", application.Name)
              .Replace("@lastname", application.LastName)
              .Replace("@email", application.Email)
              .Replace("@phone", application.Phone)
              .Replace("@flowname", flow.CustomName)
              .Replace("@flowstartdate", flow.StartDateStr)
              .Replace("@coursename", course.CourseName)
              .Replace("@confirmLink", confirmationLink)
              .Replace("@cancelLink", cancelLink)
              .Replace("@editLink", editLink)
              .Replace("@starttime", startTime)
              .Replace("@mondaytime", GetSelectedTime(application, DayOfWeek.Monday))
              .Replace("@tuesdaytime", GetSelectedTime(application, DayOfWeek.Tuesday))
              .Replace("@wednesdaytime", GetSelectedTime(application, DayOfWeek.Wednesday))
              .Replace("@thursdaytime", GetSelectedTime(application, DayOfWeek.Thursday))
              .Replace("@fridaytime", GetSelectedTime(application, DayOfWeek.Friday))
              .Replace("@saturdaytime", GetSelectedTime(application, DayOfWeek.Saturday))
              .Replace("@sundaytime", GetSelectedTime(application, DayOfWeek.Sunday))
              .Replace("@paymentdate", paymnetDate)
              .Replace("@paymentamount", paymentAmount)
              .Replace("@additionalinfo", application.Message)
              .Replace("@contractnumber", contractNumber)
              .Replace("@nextpaymentamount", paymentAmount)
              .Replace("@nextpaymentdate", paymnetDate)
              .Replace("@nextpaymentperiod", paymentPeriod);
              

            String subject = this.Subject.Replace("@firstname", application.Name)
              .Replace("@lastname", application.LastName)
              .Replace("@email", application.Email)
              .Replace("@phone", application.Phone)
              .Replace("@flowname", flow.CustomName)
              .Replace("@flowstartdate", flow.StartDateStr)
              .Replace("@coursename", course.CourseName)
              .Replace("@confirmLink", confirmationLink)
              .Replace("@cancelLink", cancelLink)
              .Replace("@editLink", editLink)
              .Replace("@starttime", startTime)
              .Replace("@mondaytime", GetSelectedTime(application, DayOfWeek.Monday))
              .Replace("@tuesdaytime", GetSelectedTime(application, DayOfWeek.Tuesday))
              .Replace("@wednesdaytime", GetSelectedTime(application, DayOfWeek.Wednesday))
              .Replace("@thursdaytime", GetSelectedTime(application, DayOfWeek.Thursday))
              .Replace("@fridaytime", GetSelectedTime(application, DayOfWeek.Friday))
              .Replace("@saturdaytime", GetSelectedTime(application, DayOfWeek.Saturday))
              .Replace("@sundaytime", GetSelectedTime(application, DayOfWeek.Sunday))
              .Replace("@paymentdate", paymnetDate)
              .Replace("@paymentamount", paymentAmount)
              .Replace("@additionalinfo", application.Message)
              .Replace("@contractnumber", contractNumber)
              .Replace("@nextpaymentamount", paymentAmount)
              .Replace("@nextpaymentdate", paymnetDate)
              .Replace("@nextpaymentperiod", paymentPeriod);

            EmailVM result    = new EmailVM();
            result.From       = "info@kharkovitcourses.com";
            result.Recipients = new List<EmailRecipientVM>();
            result.SentTime   = SmartTime.Now;
            result.Status     = EmailStatus.Pending;
            result.Subject    = subject;
            result.Body       = emailText; 

            EmailRecipientVM to = new EmailRecipientVM();
            to.Recepient        = recipient;
            to.To               = true;

            result.Recipients.Add(to);

            foreach (String s in hiddenCopies)
            {
                EmailRecipientVM bcc = new EmailRecipientVM();
                bcc.Recepient = s;
                bcc.BCC       = true;
                result.Recipients.Add(bcc);
            }
            return result;
        }

        public string FIle { get; set; }
    }

    public class EmailRecipientVM
    {
        public int RecepientId { get; set; }
        public int EmailId { get; set; }
        public String Recepient { get; set; }
        public bool To { get; set; }
        public bool CC { get; set; }
        public bool BCC { get; set; }
        public bool Phone { get; set; }

    }
}
