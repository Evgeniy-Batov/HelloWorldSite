using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Models.ViewModels;

namespace WebSite.DAL.Db.Models.Models
{
    public class ScheduledPaymentDbM
    {
        public int  PaymentId      { get; set; }

        public DateTime DueDate    { get; set; }

        public int ScheduledAmount { get; set; }

        public virtual StudentDbM Student { get; set; }

        public int?     StudentId          { get; set; }

        public virtual FlowDbModel Flow { get; set; }

        public int? FlowId   { get; set; }

        public int Status { get; set; }

        public bool  IsRecurrent { get; set; }

        public int? RecurrentType { get; set; }

        public int RecurrentMoment{ get; set; }

        public int IsExpence      { get; set; }

        public String Comment { get; set; }

        public int NumberOfSentReminders { get; set; }

        public ScheduledPaymentDbM()
        {
        }

        public ScheduledPaymentDbM(ScheduledPaymentVM payment)
                :this()
        {
            this.ApplyChanges(payment);
        }

        public void ApplyChanges(ScheduledPaymentVM newValues)
        {
            this.Comment = newValues.Comment;
            this.DueDate = newValues.DueDate;
            this.IsExpence = newValues.IsExpense ? 1 : 0;
            this.ScheduledAmount = newValues.ScheduledAmount;
            this.Status = (int)newValues.Status;
            this.IsRecurrent = newValues.IsReccurent;
            this.RecurrentType = (int)newValues.RecurrentType;
            this.RecurrentMoment = newValues.RecurrentMoment;
            this.NumberOfSentReminders = newValues.NumberOfSentReminders;
        }
    }
}
