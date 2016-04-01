using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Models.BusinessModels;
using WebSite.Services;

namespace WebSite.Models.ViewModels
{
    public enum AnswerPhoneTypes
    {
        AutoWelcomeMsg = 1,
        AutoReplyMsg   = 2,
        AutoSorryMgs   = 3
    }

    public class AnswerPhone:IAnswerPhone
    {
        private const String SUPPORT_ALIAS = "Поддержка";

        public void ProcessMessageQueue(MeetingSessionVM session,IEnumerable<MeetingMessageVM> messages,AddMessageDelegate addMessageCallBack)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            if (messages == null)
            {
                throw new ArgumentNullException("messages");
            }

            if (addMessageCallBack == null)
            {
                throw new ArgumentNullException("addMessageCallBack");
            }

            uint secondsPassedSinceSessionCreation =  (uint)(SmartTime.Now - session.CreatedOn).TotalSeconds;

            if (messages.Count() == 0)
            {
                if (secondsPassedSinceSessionCreation > 30)
                {
                    addMessageCallBack(session.SessionId,new MeetingMessageVM()
                    {
                        Author      = SUPPORT_ALIAS,
                        PostedOn    = SmartTime.Now,
                        MessageText = "Добрый день, меня зовут Евгений. Если у Вас есть какие либо вопросы я с удовольствием на них отвечу",
                        MessageId = 1,
                        MessageType = (int)AnswerPhoneTypes.AutoWelcomeMsg
                    });
                }
            }
            else
            {
                MeetingMessageVM lastMessage       = messages.OrderBy(m => m.PostedOn).LastOrDefault();
                uint secondsPassedSinceLastMessage = (uint)(SmartTime.Now - lastMessage.PostedOn).TotalSeconds;

                if (secondsPassedSinceLastMessage > 45 && messages.FirstOrDefault(m=>m.MessageType == (int)AnswerPhoneTypes.AutoSorryMgs) == null)
                {
                    if (messages.FirstOrDefault(m => m.MessageType == null) != null)
                    {
                        if (lastMessage.Author.Equals(SUPPORT_ALIAS) && lastMessage.MessageType == null)
                        {
                            //skip required
                        }
                        else
                        {
                            addMessageCallBack(session.SessionId, new MeetingMessageVM()
                            {
                                Author = SUPPORT_ALIAS,
                                PostedOn = SmartTime.Now,
                                MessageText = "Извините за ожидание, но похоже что все все операторы заняты. Оставте нам свой номер телефона, и мы созвонимся с Вами при первой же возможности. Спасибо!",
                                MessageType = (int)AnswerPhoneTypes.AutoSorryMgs
                            });
                        }
                    }
                }

                if (!lastMessage.Author.Equals(SUPPORT_ALIAS))
                {
                    if (secondsPassedSinceLastMessage > 30 && messages.FirstOrDefault(m => m.MessageType == (int)AnswerPhoneTypes.AutoReplyMsg) == null)
                    {
                        addMessageCallBack(session.SessionId, new MeetingMessageVM()
                        {
                            Author = SUPPORT_ALIAS,
                            PostedOn = SmartTime.Now,
                            MessageText = "Спасибо за проявленный интерес к нашему сайту. Идёт соединение с оператором...",
                            MessageType = (int)AnswerPhoneTypes.AutoReplyMsg
                        });
                    }
                }
            }
        }
    }
}