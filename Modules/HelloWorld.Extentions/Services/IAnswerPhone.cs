using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Models;

namespace WebSite.Services
{
    public delegate void AddMessageDelegate(Guid sessionId,MeetingMessageVM newMessage);

    public interface IAnswerPhone
    {
        void ProcessMessageQueue(MeetingSessionVM session,IEnumerable<MeetingMessageVM> messages,AddMessageDelegate addMessageCallBack);
    }
}
