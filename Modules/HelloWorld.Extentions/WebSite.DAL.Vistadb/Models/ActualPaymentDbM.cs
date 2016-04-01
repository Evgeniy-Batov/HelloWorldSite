using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Models.ViewModels;

namespace WebSite.DAL.Db.Models.Models
{
    public class ActualPaymentDbM
    {
        public ActualPaymentDbM()
        {
        }

        public void ApplyChanges(EditIncomeVM income)
        {
            this.Amount = (int)(income.Amount * 100.00m);
            this.Comment = income.Comment;
            this.IsIncome = true;
            this.PaymentDate = income.PaymentDate;
            this.PaymentId = income.PaymentId;
            this.PaymentType = (int)income.PaymentType;
            this.RegisteredBy = "admin";

            EditExpenseVM expense = income as EditExpenseVM;

            if (expense != null && (expense.ExpenseType == ExpenseType.MoneyBack || expense.ExpenseType == ExpenseType.Salary))
            {
                if (income.SelectedCourseId.HasValue && income.SelectedCourseId.Value > 0)
                    this.CourseId = income.SelectedCourseId;

                if (income.SelectedFlowId.HasValue && income.SelectedFlowId.Value > 0)
                    this.FlowId = income.SelectedFlowId;

                if (expense.ExpenseType == ExpenseType.MoneyBack)
                {
                    if (income.SelectedStudentId.HasValue && income.SelectedStudentId.Value > 0)
                        this.StudentId = income.SelectedStudentId;
                }
            }

            else if (income.PaymentType == IncomeType.StudentPayment)
            {
                if (income.SelectedCourseId.HasValue && income.SelectedCourseId.Value > 0)
                    this.CourseId = income.SelectedCourseId;

                if (income.SelectedFlowId.HasValue && income.SelectedFlowId.Value > 0)
                    this.FlowId = income.SelectedFlowId;

                if (income.SelectedStudentId.HasValue && income.SelectedStudentId.Value > 0)
                    this.StudentId = income.SelectedStudentId;
            }

            if (income.ScheduledPaymentId.HasValue && income.ScheduledPaymentId.Value < 1)
                this.ScheduledPaymentId = null;
            else
                this.ScheduledPaymentId = income.ScheduledPaymentId;
        }

        public ActualPaymentDbM(EditExpenseVM expense)
            :this()
        {
            ApplyChanges(expense);

            this.PaymentType = (int)expense.ExpenseType;
            this.IsIncome    = false;
        }

        public ActualPaymentDbM(EditIncomeVM income)
            :this()
        {
            ApplyChanges(income);
        }
    

        public int PaymentId        { get; set; }

        public int Amount           { get; set; }

        public DateTime PaymentDate { get; set; }

        public String RegisteredBy  { get; set; }

        public bool IsIncome        { get; set; }

        public int? ScheduledPaymentId { get; set; }

        public virtual ScheduledPaymentDbM ScheduledPayment { get; set; }

        public int? StudentId { get; set; }

        public virtual StudentDbM Student { get; set; }

        public int? FlowId { get; set; }

        public virtual FlowDbModel Flow { get; set; }

        public int? CourseId { get; set; }

        public virtual CourseDbM Course { get; set; }

        public String Comment { get; set; }
        
        public int PaymentType { get; set; }
    }
}
