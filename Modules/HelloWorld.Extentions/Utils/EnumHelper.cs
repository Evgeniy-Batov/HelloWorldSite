using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Common.Models.ViewModels;

namespace WebSite.Helpers
{
    public class EnumHelper
    {
        public static String ToText(ApplicationStatus status)
        {
            switch (status)
            {
                case ApplicationStatus.Approved:
                    return "Анкета подтвреждена";
                case ApplicationStatus.Sent:
                    return "Ожидает подтверждения";
                default:
                    return "Неизвестно";
            }
        }
    }
}