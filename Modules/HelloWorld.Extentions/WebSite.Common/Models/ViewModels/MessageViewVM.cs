using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Common.Models.ViewModels
{
    public class MessageViewVM
    {
        public static MessageViewVM NoMessage
        {
            get
            {
                return new MessageViewVM() { Message = "Для данного сеанса нет сообщений", Title = "Сообщения отсутствуют" };
            }
        }

        public String Title { get; set; }
        public String Message { get; set; }
    }
}
