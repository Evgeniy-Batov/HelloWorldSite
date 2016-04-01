using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.DAL.Db.Models;
using WebSite.Models;
using WebSite.Models.BusinessModels;
using WebSite.Repositories;

namespace WebSite.Db
{
    public class DbOnlineChatRepository : IOnlineChatRepository
    {
        private Context _dbContext;

        private MeetingMessageVM MessageDb2ViewModel(ChatSessionMessage dbModel)
        {
            MeetingMessageVM viewModel = new MeetingMessageVM()
            {
                Author = dbModel.Author,
                MessageId = dbModel.PostOrderIndex, //(ulong)dbModel.MessageId,
                MessageText = dbModel.MessageText,
                PostedOn = dbModel.PostedOn,
                MessageType = dbModel.MessageType
            };

            return viewModel;
        }

        private MeetingSessionVM SessionDb2ViewModel(ChatSession dbModel)
        {
            return SessionDb2ViewModel(dbModel, null);
        }

        private MeetingSessionVM SessionDb2ViewModel(ChatSession dbModel,uint? messagesCount )
        {
            if (dbModel == null)
                return null;

            MeetingSessionVM viewModel = new MeetingSessionVM()
            {
                CreatedOn = dbModel.CreatedOn,
                SessionId = dbModel.SessionId,
                LastMessageId = messagesCount.HasValue ? messagesCount.Value : (uint)dbModel.ChatSessionMessages.Count,
                IsBotProcessing  = dbModel.IsBotProcessing
            };

            return viewModel;
        }


        public DbOnlineChatRepository(String connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string should be specified");
            }

            _dbContext = new Context(connectionString);
        }

        public IEnumerable<WebSite.Models.MeetingSessionVM> GetActiveSessions()
        {
            List<MeetingSessionVM> meetingsSessions = new List<MeetingSessionVM>();
            foreach (ChatSession chat in _dbContext.ChatSessions.Include("ChatSessionMessages"))
            {
                meetingsSessions.Add(SessionDb2ViewModel(chat));
            }
            return meetingsSessions;
        }

        public WebSite.Models.MeetingMessageVM AddMessageToSession(Guid session, WebSite.Models.MeetingMessageVM message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            ChatSession dbSession = _dbContext.ChatSessions.FirstOrDefault(c=>c.SessionId.Equals(session));

            if (dbSession == null)
                throw new ArgumentOutOfRangeException("session no longer exists");

            //Check amount of repies and amount of already published messages within last hour
            if (!message.Author.Equals("Поддержка") && dbSession.ChatSessionMessages.Count(m => m.MessageType == null && m.Author.Equals("Поддержка")) == 0)
            {
                //Anti spam check
                if (dbSession.ChatSessionMessages.Count() > 15)
                    throw new InvalidOperationException("Too many noise in this session");
            }

            ChatSessionMessage dbMessage = (new ChatSessionMessage(){
                Author      = message.Author,
                MessageText = message.MessageText,
                PostedOn    = message.PostedOn,
                MessageType = message.MessageType
            });

            dbSession.ChatSessionMessages.Add(dbMessage);

            _dbContext.SaveChanges();

            return MessageDb2ViewModel(dbMessage);
        }

        public WebSite.Models.MeetingSessionVM CreateMeetingSession(String requestorIp)
        {
            ChatSession lastSessionFromIp = _dbContext.ChatSessions.Where(s => s.IP.Equals(requestorIp)).OrderByDescending(s => s.CreatedOn).FirstOrDefault();

            if (lastSessionFromIp != null && (SmartTime.Now - lastSessionFromIp.CreatedOn).TotalMinutes < 15)
            {
                return SessionDb2ViewModel(lastSessionFromIp, 0);
            }

            ChatSession session = new ChatSession()
            {
                CreatedOn = SmartTime.Now,
                SessionId = Guid.NewGuid(),
                IP        = requestorIp,
                IsBotProcessing = false
            };

            _dbContext.ChatSessions.Add(session);
            _dbContext.SaveChanges();

            return SessionDb2ViewModel(session);
        }

        public WebSite.Models.MeetingSessionVM GetMeetingSessionById(Guid meetingSession)
        {
            ChatSession session = _dbContext.ChatSessions.FirstOrDefault(c => c.SessionId.Equals(meetingSession));
            return SessionDb2ViewModel(session);
        }

        public void DeleteSession(Guid meetingSession)
        {
            ChatSession session = _dbContext.ChatSessions.FirstOrDefault(s => s.SessionId.Equals(meetingSession));
            if (session != null)
            {
                _dbContext.ChatSessions.Remove(session);
                _dbContext.SaveChanges();
            }
        }

        public void ArchiveSession(WebSite.Models.MeetingSessionVM session, IEnumerable<WebSite.Models.MeetingMessageVM> messages)
        {
            //TODO: Temporary call. It is needed to implement honest archiving later 
            //this.DeleteSession(session.SessionId);
        }

        public IEnumerable<WebSite.Models.MeetingMessageVM> LoadSessionMessages(Guid session, ulong startingFrom)
        {
            IQueryable<ChatSessionMessage> messages = _dbContext.ChatSessionMessages
                .Where(m => m.SessionId.Equals(session)).OrderBy(m => m.PostedOn);

            List<MeetingMessageVM> result = new List<MeetingMessageVM>();
            ulong counter = 0;
            foreach (ChatSessionMessage cm in messages)
            {
                if (++counter <= startingFrom)
                    continue;

                cm.PostOrderIndex = counter;
                result.Add(MessageDb2ViewModel(cm));
            }
            return result;
        }


        public bool StartBotProcessing(Guid sessionId)
        {
            ChatSession session = _dbContext.ChatSessions.FirstOrDefault(s => s.SessionId.Equals(sessionId) && !s.IsBotProcessing);
            if (session != null)
            {
                session.IsBotProcessing = true;
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public void EndBotProcessing(Guid sesionId)
        {
            ChatSession session = _dbContext.ChatSessions.FirstOrDefault(s => s.SessionId.Equals(sesionId));
            if (session != null)
            {
                session.IsBotProcessing = false;
                _dbContext.SaveChanges();
            }
        }
    }
}
