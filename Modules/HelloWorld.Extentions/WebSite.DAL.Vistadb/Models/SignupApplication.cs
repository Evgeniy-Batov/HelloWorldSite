using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.DAL.Db.Models
{
    public class SignupApplicationDbModel
    {
        public virtual FlowDbModel Flow { get; set; }

        public int ApplicationId { get; set; }
        public int FlowId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Refferal { get; set; }
        public int MondayTime { get; set; }
        public int TuesdayTime { get; set; }
        public int WednesdayTime { get; set; }
        public int ThursdayTime { get; set; }
        public int FridayTime { get; set; }
        public int SaturdayTime { get; set; }
        public int SundayTime { get; set; }
        public String AdditionalInfo { get; set; }
        public DateTime CreatedOn { get; set; }
        public String IP { get; set; }
        public int Status { get; set; }
        public Guid AccessToken { get; set; }

        public void ApplyChanges(WebSite.Common.Models.ViewModels.SignupApplication vm,FlowDbModel newFlow)
        {
            this.FlowId = vm.FlowId;
            this.FirstName = vm.Name;
            this.LastName = vm.LastName;
            this.Email = vm.Email;
            this.Phone = vm.Phone;
            this.Refferal = vm.SelectedReferral;
            this.MondayTime    = vm.Schedule.GetDesiredTimeAsInt(DayOfWeek.Monday);
            this.TuesdayTime   = vm.Schedule.GetDesiredTimeAsInt(DayOfWeek.Tuesday);
            this.WednesdayTime = vm.Schedule.GetDesiredTimeAsInt(DayOfWeek.Wednesday);
            this.ThursdayTime  = vm.Schedule.GetDesiredTimeAsInt(DayOfWeek.Thursday);
            this.FridayTime    = vm.Schedule.GetDesiredTimeAsInt(DayOfWeek.Friday);
            this.SaturdayTime  = vm.Schedule.GetDesiredTimeAsInt(DayOfWeek.Saturday);
            this.SundayTime    = vm.Schedule.GetDesiredTimeAsInt(DayOfWeek.Sunday);
            this.Status         = (int)vm.Status;

            if (newFlow != null)
                this.Flow = newFlow;

            this.AdditionalInfo = vm.Message;
        }
    }

    public class CanceledSignupApplicationDbModel
    {
        public virtual FlowDbModel Flow { get; set; }

        public int ApplicationId { get; set; }
        public int FlowId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Refferal { get; set; }
        public int MondayTime { get; set; }
        public int TuesdayTime { get; set; }
        public int WednesdayTime { get; set; }
        public int ThursdayTime { get; set; }
        public int FridayTime { get; set; }
        public int SaturdayTime { get; set; }
        public int SundayTime { get; set; }
        public String AdditionalInfo { get; set; }
        public DateTime CreatedOn { get; set; }
        public String IP { get; set; }
        public int Status { get; set; }
        public Guid AccessToken { get; set; }


        public CanceledSignupApplicationDbModel()
        {
        }

        public CanceledSignupApplicationDbModel(SignupApplicationDbModel model)
            :this()
        {
            this.ApplicationId = 0;
            this.FlowId = model.FlowId;
            this.FirstName = model.FirstName;
            this.LastName = model.LastName;
            this.Email = model.Email;
            this.Phone =model.Phone;
            this.Refferal = model.Refferal;
            this.MondayTime = model.MondayTime ;
            this.TuesdayTime = model.TuesdayTime;
            this.WednesdayTime =model.WednesdayTime;
            this.ThursdayTime = model.ThursdayTime;
            this.FridayTime = model.FridayTime;
            this.SaturdayTime = model.SaturdayTime;
            this.SundayTime = model.SundayTime;
            this.Status =model.Status ;
            this.AdditionalInfo = model.AdditionalInfo;
            this.CreatedOn = model.CreatedOn;
            this.IP = model.IP;
            this.AccessToken = model.AccessToken;
        }
    }
}
