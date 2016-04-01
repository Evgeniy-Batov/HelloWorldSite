using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Repositories
{
    public interface IOnlineChatRepository
    {
        IEnumerable<WebSite.Models.MeetingSessionVM> GetActiveSessions();
        WebSite.Models.MeetingMessageVM AddMessageToSession(Guid session, WebSite.Models.MeetingMessageVM message);
        WebSite.Models.MeetingSessionVM CreateMeetingSession(String requestorIP);
        WebSite.Models.MeetingSessionVM GetMeetingSessionById(Guid meetingSession);
        void DeleteSession(Guid meetingSession);
        void ArchiveSession(WebSite.Models.MeetingSessionVM session, IEnumerable<WebSite.Models.MeetingMessageVM> messages);
        System.Collections.Generic.IEnumerable<WebSite.Models.MeetingMessageVM> LoadSessionMessages(Guid session, ulong startingFrom);
        bool StartBotProcessing(Guid sessionId);
        void EndBotProcessing(Guid sesionId);
    }
}
