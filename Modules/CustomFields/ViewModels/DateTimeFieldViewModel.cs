using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomFields.DateTimeField.ViewModels
{
    public class DateTimeFieldViewModel
    {
        public string Name { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }

        public bool ShowDate { get; set; }
        public bool ShowTime { get; set; }
    }
}