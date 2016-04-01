using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Interfaces.Repositories;
using WebSite.Common.Models.ViewModels;
using WebSite.DAL.Db.Models.Models;
using WebSite.ViewModels;
using System.Data.Entity;
using WebSite.DAL.Db.Repositories;
using WebSite.DAL.Vistadb.Repositories;

namespace WebSite.DAL.Db.Models.Repositories
{
    public class DbStudentRepository : IStudentRepository
    {
        private Context _dbContext;

        public DbStudentRepository(String connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentOutOfRangeException("connectionString");
            }
            _dbContext = new Context(connectionString);
        }

        public static IEnumerable<ScheduledPaymentVM> DbModel2ViewModel(IEnumerable<ScheduledPaymentDbM> models)
        {
            List<ScheduledPaymentVM> results = new List<ScheduledPaymentVM>();
            foreach (ScheduledPaymentDbM s in models)
                results.Add(DbModel2ViewModel(s));

            return results;
        }

        public static ScheduledPaymentVM DbModel2ViewModel(ScheduledPaymentDbM payment,bool mapRelated = false)
        {
            ScheduledPaymentVM result = new ScheduledPaymentVM();
            result.Comment = payment.Comment;
            result.DueDate = payment.DueDate;
            result.IsExpense = payment.IsExpence > 0;
            result.IsReccurent = payment.IsRecurrent;
            result.PaymentId = payment.PaymentId;
            result.RecurrentMoment = payment.RecurrentMoment;
            result.NumberOfSentReminders = payment.NumberOfSentReminders;
            
            if (payment.RecurrentType.HasValue)
                result.RecurrentType = (ScheduledPaymentRecurrent)payment.RecurrentType.Value;
            else
                result.RecurrentType = ScheduledPaymentRecurrent.Unknown;

            result.ScheduledAmount = payment.ScheduledAmount;
            result.Status = (ScheduledPaymentStatus)payment.Status;

            if (mapRelated)
            {
                if (payment.Student != null)
                {
                    StudentVM std      = DbModel2ViewModel(payment.Student);
                    result.StudentId   = std.StudentId;
                    result.StudentName = std.FullName;
                }

                if (payment.Flow != null)
                {
                    result.Flow = DbCourseRepository.DbModel2ViewModel(payment.Flow);
                }
            }

            return result;
        }

        public static IEnumerable<StudentVM> DbModel2ViewModel(IEnumerable<StudentDbM> student)
        {
            List<StudentVM> results = new List<StudentVM>();
            foreach (StudentDbM s in student)
                results.Add(DbModel2ViewModel(s));

            return results;
        }

        public static StudentVM DbModel2ViewModel(StudentDbM student)
        {
            StudentVM result = new StudentVM();
            result.Comment = student.Comment;
            result.ContractNumber = student.ContractNumber;
            result.Email = student.Email;
            result.FirstName = student.FirstName;
            result.FlowId = student.FlowId;
            result.INN = student.INN;
            result.LastName = student.LastName;
            result.MiddleName = student.MiddleName;
            result.PasspordIssuedDate = student.PasspordIssuedDate;
            result.PassportIssuedBy = student.PassportIssuedBy;
            result.PassportIssuedAt = student.PassportIssuedAt;
            result.PassportNo = student.PassportNo;
            result.PaymentModel = (StudentPaymentModel)student.PaymentModel;
            result.Phone1 = student.Phone1;
            result.Phone2 = student.Phone2;
            result.StudentId = student.StudentId;
            result.StudentSince = student.StudentSince;
            result.StudentTill = student.StudentTill;
            result.Status = (StudentStatus)student.Status;

            foreach (var payment in student.ScheduledPayments)
                result.Payments.Add(DbModel2ViewModel(payment));

            return result;
        }


        public IEnumerable<StudentVM> LoadFlowStudents(int flowId)
        {
            IEnumerable<StudentDbM> students = _dbContext.Students.Where(s => s.FlowId.Equals(flowId)).ToList();

            return DbModel2ViewModel(students);
        }

        public ScheduledPaymentVM LoadPayment(int paymentId)
        {
            ScheduledPaymentDbM payment = _dbContext.ScheduledPayments.Include(p=>p.Student).Include(p=>p.Flow).FirstOrDefault(p => p.PaymentId.Equals(paymentId));

            return DbModel2ViewModel(payment,true);
        }

        public StudentVM LoadStudent(int studentId)
        {
            StudentDbM std = _dbContext.Students.Include(s=>s.ScheduledPayments).FirstOrDefault(s => s.StudentId.Equals(studentId));

            return DbModel2ViewModel(std);
        }

        public void SaveStudent(StudentVM studentData, IEnumerable<ScheduledPaymentVM> payments)
        {
            if (studentData.StudentId > 0)
            {
                StudentDbM existingStudent = _dbContext.Students.FirstOrDefault(s => s.StudentId.Equals(studentData.StudentId));
                existingStudent.ApplyChanges(studentData);
            }
            else
            {
                StudentDbM newStudent = new StudentDbM(studentData);

                if (payments != null)
                {
                    foreach(ScheduledPaymentVM payment in payments)
                    {
                        _dbContext.ScheduledPayments.Add(new ScheduledPaymentDbM()
                        {
                            IsExpence  = 0,
                            IsRecurrent = false,
                            DueDate = payment.DueDate,
                            FlowId = studentData.FlowId,
                            ScheduledAmount = payment.ScheduledAmount,
                            Status = (int)payment.Status,
                            Student = newStudent,
                            Comment = payment.Comment
                        });
                    }
                }

                _dbContext.Students.Add(newStudent);
            }
            _dbContext.SaveChanges();
        }

        public ScheduledPaymentVM SaveScheduledPayment(ScheduledPaymentVM payment)
        {
            int paymentid = 0;

            if (payment.PaymentId > 0)
            {
                paymentid = payment.PaymentId;

                ScheduledPaymentDbM existingPayment = _dbContext.ScheduledPayments.FirstOrDefault(p => p.PaymentId.Equals(payment.PaymentId));

                existingPayment.ApplyChanges(payment);

                _dbContext.SaveChanges();
            }
            else
            {
                ScheduledPaymentDbM newModel = new ScheduledPaymentDbM(payment);

                if (payment.StudentId > 0)
                    newModel.Student = _dbContext.Students.FirstOrDefault(s => s.StudentId.Equals(payment.StudentId));

                if (payment.Flow != null)
                    newModel.Flow    = _dbContext.Students.FirstOrDefault(s => s.StudentId.Equals(payment.StudentId)).Flow;

                _dbContext.ScheduledPayments.Add(newModel);

                _dbContext.SaveChanges();

                paymentid = newModel.PaymentId;
            }

            return LoadPayment(paymentid);
        }



        public static IEnumerable<EditIncomeVM> DbModel2IncomeModel(IEnumerable<ActualPaymentDbM> payments)
        {
            List<EditIncomeVM> result = new List<EditIncomeVM>();
            foreach (ActualPaymentDbM payment in payments)
                result.Add(DbModel2IncomeModel(payment));

            return result;
        }

        public static IEnumerable<EditExpenseVM> DbModel2ExpenseModel(IEnumerable<ActualPaymentDbM> payments)
        {
            List<EditExpenseVM> result = new List<EditExpenseVM>();
            foreach (ActualPaymentDbM payment in payments)
                result.Add(DbModel2ExpenseModel(payment));

            return result;
        }

        public static EditExpenseVM DbModel2ExpenseModel(ActualPaymentDbM payment)
        {
            EditExpenseVM result = new EditExpenseVM();

            result.Amount = payment.Amount;
            result.Comment = payment.Comment;
            result.PaymentId = payment.PaymentId;
            result.PaymentDate = payment.PaymentDate;
            result.ExpenseType = (ExpenseType)payment.PaymentType;
            result.RegisteredBy = payment.RegisteredBy;
            result.ScheduledPaymentId = payment.ScheduledPaymentId;

            if (payment.Student != null)
            {
                result.StudentName = payment.Student.LastName + " " + payment.Student.FirstName + " " + payment.Student.MiddleName;
                result.SelectedStudentId = payment.Student.StudentId;
            }

            if (payment.Flow != null)
            {
                result.SelectedFlowId = payment.Flow.FlowId;
                result.FlowName = String.IsNullOrEmpty(payment.Flow.CustomName) ? payment.Flow.StartDateStr : payment.Flow.CustomName;
            }

            if (payment.Course != null)
            {
                result.CourseName = payment.Course.CourseName;
                result.SelectedCourseId = payment.Course.CourseId;
            }

            return result;
        }
        
        public static EditIncomeVM  DbModel2IncomeModel(ActualPaymentDbM payment)
        {
            EditIncomeVM result = new EditIncomeVM();

            result.Amount = payment.Amount;
            result.Comment = payment.Comment;
            result.PaymentId = payment.PaymentId;
            result.PaymentDate = payment.PaymentDate;
            result.PaymentType = (IncomeType)payment.PaymentType;
            result.RegisteredBy = payment.RegisteredBy;
            result.ScheduledPaymentId = payment.ScheduledPaymentId;

            if (payment.Student != null)
            {
                result.StudentName = payment.Student.LastName + " " + payment.Student.FirstName + " " + payment.Student.MiddleName;
                result.SelectedStudentId = payment.Student.StudentId;
            }

            if (payment.Flow != null)
            {
                result.SelectedFlowId = payment.Flow.FlowId;
                result.FlowName = String.IsNullOrEmpty(payment.Flow.CustomName) ? payment.Flow.StartDateStr : payment.Flow.CustomName;
            }

            if (payment.Course != null)
            {
                result.CourseName = payment.Course.CourseName;
                result.SelectedCourseId = payment.Course.CourseId;
            }

            return result;
        }

        public EditExpenseVM SaveExpense(EditExpenseVM expense)
        {
            if (expense.PaymentId < 1)
            {
                ActualPaymentDbM newRecord = new ActualPaymentDbM(expense);

                _dbContext.ActualPayments.Add(newRecord);

                _dbContext.SaveChanges();

                return DbModel2ExpenseModel(_dbContext.ActualPayments.FirstOrDefault(p => p.PaymentId.Equals(newRecord.PaymentId)));
            }
            else
            {
                ActualPaymentDbM record = _dbContext.ActualPayments.FirstOrDefault(p => p.PaymentId.Equals(expense.PaymentId));

                record.ApplyChanges(expense);

                _dbContext.SaveChanges();

                return DbModel2ExpenseModel(record);
            }
        }


        public EditIncomeVM SaveIncome(EditIncomeVM income)
        {
            if (income.PaymentId < 1)
            {
                ActualPaymentDbM newRecord = new ActualPaymentDbM(income);

                _dbContext.ActualPayments.Add(newRecord);

                _dbContext.SaveChanges();

                return DbModel2IncomeModel(_dbContext.ActualPayments.FirstOrDefault(p => p.PaymentId.Equals(newRecord.PaymentId)));
            }
            else
            {
                ActualPaymentDbM record = _dbContext.ActualPayments.FirstOrDefault(p => p.PaymentId.Equals(income.PaymentId));

                record.ApplyChanges(income);

                _dbContext.SaveChanges();

                return DbModel2IncomeModel(record);
            }
        }

        private List<ActualPaymentDbM> LoadActualPayments(DateTime? from, DateTime? to, int? courseId, int? flowId, int? studentId, IPageRequest pageRequest,bool? loadIncomes)
        {
            ulong totalIncomes = 0;

            if (loadIncomes.HasValue)
            {
                totalIncomes = (uint)_dbContext.ActualPayments.Where(p => p.IsIncome.Equals(loadIncomes.Value)).Count();
            }
            else
            {
                totalIncomes = (uint)_dbContext.ActualPayments.Count();
            }
            

            ushort pageCount = (ushort)(totalIncomes / pageRequest.PageSize + 1);

            ushort requestedPage = pageRequest.PageNumber;
            if (requestedPage < pageCount)
                requestedPage = pageCount;

            IQueryable<ActualPaymentDbM> cursor = null;

            if (loadIncomes.HasValue)
            {
                cursor = _dbContext.ActualPayments.Where(p => p.IsIncome.Equals(loadIncomes.Value));
            }
            else
            {
                cursor = _dbContext.ActualPayments;
            }

            if (from.HasValue)
            {
                cursor = cursor.Where(c => c.PaymentDate >= from.Value);
            }
            if (to.HasValue)
            {
                cursor = cursor.Where(c => c.PaymentDate <= to.Value);
            }
            if (courseId.HasValue && courseId.Value > 0)
            {
                cursor = cursor.Where(c => c.CourseId.HasValue && c.CourseId.Value.Equals(courseId.Value));
            }
            if (flowId.HasValue && flowId.Value > 0)
            {
                cursor = cursor.Where(c => c.FlowId.HasValue && c.FlowId.Value.Equals(flowId.Value));
            }
            if (studentId.HasValue && studentId.Value > 0)
            {
                cursor = cursor.Where(c => c.StudentId.HasValue && c.StudentId.Value.Equals(studentId.Value));
            }

            List<ActualPaymentDbM> dbObjects =
                cursor.OrderByDescending(m => m.PaymentDate).Skip(pageRequest.PageSize * (pageRequest.PageNumber - 1)).Take(pageRequest.PageSize).ToList();

            return dbObjects;
        }

        public IPagedResult<EditIncomeVM> LoadIncomes(DateTime? from, DateTime? to, int? courseId, int? flowId, int? studentId, IPageRequest pageRequest)
        {
            PagedResult<EditIncomeVM> result = null;

            List<ActualPaymentDbM> dbObjects = LoadActualPayments(from, to, courseId, flowId, studentId, pageRequest,true);

            result = new PagedResult<EditIncomeVM>(DbModel2IncomeModel(dbObjects), pageRequest.PageSize, pageRequest.PageNumber,pageRequest.PageSize,pageRequest.PageSize);

            return result ;
        }

        public IPagedResult<EditExpenseVM> LoadExpenses(DateTime? from, DateTime? to, int? courseId, int? flowId, int? studentId, IPageRequest pageRequest)
        {
            PagedResult<EditExpenseVM> result = null;

            List<ActualPaymentDbM> dbObjects = LoadActualPayments(from, to, courseId, flowId, studentId, pageRequest,false);

            result = new PagedResult<EditExpenseVM>(DbModel2ExpenseModel(dbObjects), pageRequest.PageSize, pageRequest.PageNumber, pageRequest.PageSize, pageRequest.PageSize);

            return result;
        }


        public EditIncomeVM LoadIncome(int incomeId)
        {
            return DbModel2IncomeModel(_dbContext.ActualPayments.FirstOrDefault(i => i.PaymentId.Equals(incomeId) && i.IsIncome));
        }

        public void DeleteIncome(int incomeId)
        {
            _dbContext.ActualPayments.Remove(_dbContext.ActualPayments.FirstOrDefault(i => i.PaymentId.Equals(incomeId) && i.IsIncome));
            _dbContext.SaveChanges();
        }

        public void DeleteExpense(int expenseId)
        {
           ActualPaymentDbM payment =  _dbContext.ActualPayments.FirstOrDefault(p => !p.IsIncome && p.PaymentId.Equals(expenseId));

            if (payment != null)
            {
                _dbContext.ActualPayments.Remove(payment);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteStudent(int studentId)
        {
            StudentDbM std = _dbContext.Students.FirstOrDefault(s => s.StudentId.Equals(studentId));
            if (std != null)
            {
                foreach (ScheduledPaymentDbM payment in std.ScheduledPayments)
                {
                    if (payment.Status == (int)ScheduledPaymentStatus.Scheduled || payment.Status == (int)ScheduledPaymentStatus.Rescheduled || payment.Status == (int)ScheduledPaymentStatus.Unknown)
                        payment.Status = (int)ScheduledPaymentStatus.Canceled;
                }

                std.Status      = (int)StudentStatus.Excluded;
                std.StudentTill = DateTime.Today;
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<ScheduledPaymentVM> LoadPayments(int year, int month)
        {
            IEnumerable<ScheduledPaymentDbM> payments = _dbContext.ScheduledPayments
                .OrderBy(s=>s.DueDate)
                .Where(s => s.DueDate.Month.Equals(month) && s.DueDate.Year.Equals(year));

            return DbModel2ViewModel(payments);
        }

        public EditExpenseVM LoadExpense(int expenseId)
        {
            ActualPaymentDbM payment = _dbContext.ActualPayments.FirstOrDefault(p => p.PaymentId.Equals(expenseId));

            return DbModel2ExpenseModel(payment);
        }
    }
}
