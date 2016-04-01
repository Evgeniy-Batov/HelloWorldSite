using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Models.ViewModels;
using WebSite.ViewModels;

namespace WebSite.Common.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        void SaveStudent(StudentVM studentData, IEnumerable<ScheduledPaymentVM> payments);

        IEnumerable<StudentVM> LoadFlowStudents(int flowId);

        StudentVM LoadStudent(int studentId);

        IEnumerable<ScheduledPaymentVM> LoadPayments(int yeat, int month);

        ScheduledPaymentVM LoadPayment(int paymentId);

        ScheduledPaymentVM SaveScheduledPayment(ScheduledPaymentVM payment);

        EditIncomeVM LoadIncome(int incomeId);

        EditExpenseVM LoadExpense(int expenseId);

        void DeleteIncome(int incomeId);
        
        IPagedResult<EditIncomeVM> LoadIncomes(DateTime? from, DateTime? to, int? courseId, int? flowId, int? studentId, IPageRequest pageRequest);

        IPagedResult<EditExpenseVM> LoadExpenses(DateTime? from, DateTime? to, int? courseId, int? flowId, int? studentId, IPageRequest pageRequest);

        void DeleteStudent(int studentId);

        void DeleteExpense(int expenseId);

        EditIncomeVM SaveIncome(EditIncomeVM income);

        EditExpenseVM SaveExpense(EditExpenseVM expense);
    }
}
