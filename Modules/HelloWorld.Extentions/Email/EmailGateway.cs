using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using WebSite.Common.Interfaces.Repositories;
using WebSite.Common.Models.ViewModels;
using WebSite.DAL.Db.Models.Repositories;
using WebSite.Models.BusinessModels;

namespace WebSite.Email
{
    public class EmailGateway
    {
        private String _connStr;
        private readonly SMSWorker _smsWorker = new SMSWorker();
        
        public EmailGateway(String connection)
        {
            _connStr = connection;
        }

        private void SendEmail(EmailVM emailVM,IEmailRepository repository)
        {
            try
            {
                emailVM.Status   = EmailStatus.Sending;
                repository.UpdateEmail(emailVM);

                MailMessage message = new MailMessage();
                message.From        = new MailAddress(emailVM.From);
                message.Subject     = emailVM.Subject;
                message.Body        = emailVM.Body;
                message.IsBodyHtml  = emailVM.IsHtml;

                foreach (EmailRecipientVM recipient in emailVM.Recipients)
                {
                    if (recipient.To)
                    {
                        message.To.Add(new MailAddress(recipient.Recepient));
                    }
                    else if (recipient.CC)
                    {
                        message.CC.Add(new MailAddress(recipient.Recepient));
                    }
                    else if (recipient.BCC)
                    {
                        message.Bcc.Add(new MailAddress(recipient.Recepient));
                    }
                    else if (recipient.Phone)
                    {
                        try
                        {
                            string something = _smsWorker.Auth(ConfigurationManager.AppSettings["SMSLogin"], ConfigurationManager.AppSettings["SMSPassword"]);

                            _smsWorker.SendSMS(ConfigurationManager.AppSettings["SMSSenderName"], recipient.Recepient, message.Body, "");
                        }
                        catch
                        {

                        }
                    }
                }

                if (message.To.Count != 0)
                {
                    using (SmtpClient smtp = new SmtpClient())
                    {

                        smtp.Host = "smtpout.secureserver.net";
                        smtp.Port = 25;
                        smtp.Timeout = 10000;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("info@kharkovitcourses.com", "ajhvbhjdfybt");
                        smtp.Send(message);
                    }
                }

                emailVM.SentTime = SmartTime.Now;
                emailVM.Status   = EmailStatus.Sent;
            }
            catch
            {
                emailVM.Status = EmailStatus.Pending;
            }
            finally
            {
                repository.UpdateEmail(emailVM);
            }
        }

        public void SendAllPending()
        {
            IEmailRepository repo = new DbEmailRepository(_connStr);

            ICollection<EmailVM> emails = repo.LoadEmails(EmailStatus.Pending);

            Task.Factory.StartNew(() => Parallel.ForEach<EmailVM>(emails, e => SendEmail(e, new DbEmailRepository(_connStr))));
        }

    }
}