using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Interfaces.Repositories;
using WebSite.Common.Models.ViewModels;

namespace WebSite.DAL.Db.Models.Repositories
{
    public class DbEmailRepository : IEmailRepository
    {
        private Context _dbContext;

        public DbEmailRepository(String connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentOutOfRangeException("connectionString");
            }
            _dbContext = new Context(connectionString);
        }

        public ICollection<EmailVM> LoadEmails(EmailStatus status)
        {
            List<EmailVM> emails               = new List<EmailVM>();
            IQueryable<EmailDbModel> emailRows = _dbContext.Emails.Include("Recipients").Where(e => e.Status.Equals((int)status));

            foreach (EmailDbModel e in emailRows)
                emails.Add(DbModel2ViewModel(e));

            return emails;
        }

        public ICollection<EmailVM> LoadEmails(String recipient,int? top)
        {
            List<EmailVM> emails = new List<EmailVM>();
            
            IQueryable<EmailDbModel> emailRows = _dbContext.Emails.Include("Recipients")
                .Where(e => e.Recipients.Any(r=>r.Recepient.Equals(recipient)))
                .OrderByDescending(e=>e.SentTime);

            if (top.HasValue)
                emailRows = emailRows.Take(top.Value);
            
            foreach (EmailDbModel e in emailRows)
                emails.Add(DbModel2ViewModel(e));

            return emails;
        }


        public EmailRecipientVM DbModel2ViewModel(EmailRecipientDbModel recipient)
        {
            EmailRecipientVM res = new EmailRecipientVM();
            res.BCC              = recipient.BCC;
            res.CC               = recipient.CC;
            res.Recepient        = recipient.Recepient;
            res.To               = recipient.To;
            res.Phone            = recipient.Phone;

            return res;
        }

        public EmailVM DbModel2ViewModel(EmailDbModel dbModel)
        {
            if (dbModel == null)
                return null;

            EmailVM vm  = new EmailVM();
            vm.Body     = dbModel.Body;
            vm.EmailId  = dbModel.EmailId;
            vm.From     = dbModel.From;
            vm.SentTime = dbModel.SentTime;
            vm.Status   = (EmailStatus)dbModel.Status;
            vm.Subject  = dbModel.Subject;
            vm.IsHtml   = dbModel.IsHtml;

            vm.Recipients = new List<EmailRecipientVM>();
            foreach (EmailRecipientDbModel db in dbModel.Recipients)
                vm.Recipients.Add(DbModel2ViewModel(db));

            return vm;
        }

        public EmailTemplateVM DbModel2ViewModel(EmailTemplateDbModel dbModel)
        {
            if (dbModel == null)
                return null;

            EmailTemplateVM vm = new EmailTemplateVM();
            vm.TemplateId = dbModel.TemplateID;
            vm.Body = dbModel.HtmlVersion;
            vm.Subject = dbModel.TextVersion;
            return vm;
        }

        public EmailRecipientDbModel ViewModel2DbModel(EmailRecipientVM recipientVm)
        {
            EmailRecipientDbModel res = new EmailRecipientDbModel();
            res.BCC = recipientVm.BCC;
            res.CC = recipientVm.CC;
            res.EmailId = recipientVm.EmailId;
            res.Recepient = recipientVm.Recepient;
            res.To = recipientVm.To;
            res.Phone = recipientVm.Phone;
            
            return res;
        }

        public EmailDbModel ViewModel2DbModel(EmailVM email)
        {
            EmailDbModel result = new EmailDbModel();
            result.Body         = email.Body;
            result.EmailId      = email.EmailId;
            result.From         = email.From;
            result.Recipients   = new List<EmailRecipientDbModel>();
            foreach (EmailRecipientVM r in email.Recipients)
                result.Recipients.Add(ViewModel2DbModel(r));
            
            result.SentTime     = email.SentTime;
            result.Status       = (int)email.Status;
            result.Subject      = email.Subject;
            result.IsHtml       = email.IsHtml;

            return result;
        }

        public void UpdateEmail(EmailVM email)
        {
            EmailDbModel row = _dbContext.Emails.FirstOrDefault(e => e.EmailId.Equals(email.EmailId));
            if (row != null)
            {
                row.Status = (int)email.Status;
            }
            _dbContext.SaveChanges();
        }

        public EmailVM SaveEmail(EmailVM email)
        {
            EmailDbModel row = ViewModel2DbModel(email);

            _dbContext.Emails.Add(row);

            _dbContext.SaveChanges();

            return email;
        }


        public EmailTemplateVM LoadTemplate(EmailTemplate id)
        {
            return DbModel2ViewModel(_dbContext.EmailTemplates.FirstOrDefault(e => e.TemplateID.Equals((int)id)));
        }

        public ICollection<EmailVM> LoadEmails(int offset, int size)
        {
            List<EmailVM> emails = new List<EmailVM>();
            IQueryable<EmailDbModel> emailRows = _dbContext.Emails.Include("Recipients").OrderByDescending(e => e.SentTime).Take(size).Skip(offset);

            foreach (EmailDbModel e in emailRows)
                emails.Add(DbModel2ViewModel(e));

            return emails;
        }
    }
}
