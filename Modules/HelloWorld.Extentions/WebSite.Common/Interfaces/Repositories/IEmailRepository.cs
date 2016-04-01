using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Models.ViewModels;

namespace WebSite.Common.Interfaces.Repositories
{
    public interface IEmailRepository
    {
        EmailVM SaveEmail(EmailVM email);
        void UpdateEmail(EmailVM email);
        ICollection<EmailVM> LoadEmails(int offset,int size);
        ICollection<EmailVM> LoadEmails(EmailStatus status);
        ICollection<EmailVM> LoadEmails(String recipient,int? top);
        EmailTemplateVM LoadTemplate(EmailTemplate template);
    }
}
