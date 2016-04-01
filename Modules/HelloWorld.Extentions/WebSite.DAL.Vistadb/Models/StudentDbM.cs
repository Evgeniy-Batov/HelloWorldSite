using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Models.ViewModels;

namespace WebSite.DAL.Db.Models.Models
{
    public class StudentDbM
    {

        public StudentDbM()
        {

        }

        public StudentDbM(StudentVM vm)
        {
            ApplyChanges(vm);
        }

        public void ApplyChanges(StudentVM vm)
        {
            this.Comment = vm.Comment;
            this.ContractNumber = vm.ContractNumber;
            this.Email = vm.Email;
            this.FirstName = vm.FirstName;
            this.FlowId = vm.FlowId;
            this.Flow = null;
            this.INN = vm.INN;
            this.LastName = vm.LastName;
            this.MiddleName = vm.MiddleName;
            this.PasspordIssuedDate = vm.PasspordIssuedDate;
            this.PassportIssuedAt = vm.PassportIssuedAt;
            this.PassportIssuedBy = vm.PassportIssuedBy;
            this.PassportNo = vm.PassportNo;
            this.PaymentModel = (int)vm.PaymentModel;
            this.Phone1 = vm.Phone1;
            this.Phone2 = vm.Phone2;
            this.StudentId = vm.StudentId;
            this.StudentSince = vm.StudentSince;
            this.StudentTill = vm.StudentTill;
            this.Status = (int)vm.Status;
        }

        public int StudentId { get; set; }

        public DateTime  StudentSince { get; set; }
        public DateTime? StudentTill  { get; set; }

        public int FlowId { get; set; }

        public virtual FlowDbModel Flow { get; set; }

        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Phone1 { get; set; }
        public String Phone2 { get; set; }

        public String PassportNo { get; set; }
        public DateTime PasspordIssuedDate { get; set; }
        public String PassportIssuedAt { get; set; }
        public String PassportIssuedBy { get; set; }
        public String INN { get; set; }

        public String ContractNumber { get; set; }

        public int PaymentModel { get; set; }

        public String Comment { get; set; }

        public virtual List<ScheduledPaymentDbM> ScheduledPayments { get; set; }

        public int Status { get; set; }
    }
}
