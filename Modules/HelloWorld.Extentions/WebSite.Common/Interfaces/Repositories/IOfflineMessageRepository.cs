using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Models.ViewModels;

namespace WebSite.Common.Interfaces.Repositories
{
    public interface IOfflineMessageRepository
    {
        IPagedResult<OfflineMessage> LoadPaged(IPageRequest pageRequest);
        IEnumerable<OfflineMessage> LoadMessages(DateTime? startingFrom);
        void AddMessage(OfflineMessage message);

       SignupApplication LoadApplication(int applicationId,Guid accessKey);
        SignupApplication GetApplication(int applicationId);
        SignupApplication GetApplication(string userEmail);
        SignupApplication GetApplication(string userEmail,int flowid);
        IEnumerable<SignupApplication> LoadApplicationsByFlow(int flowId,ApplicationStatus? statusFilter);
        void AddSignupApplication(SignupApplication application);
        void UpdateSignupApplication(SignupApplication application);
    }
}
