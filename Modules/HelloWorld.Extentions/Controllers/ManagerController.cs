using HelloWorld.Extentions.Services;
using HelloWorld.Extentions.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebSite.Common.Interfaces.Repositories;
using WebSite.Common.Models.ViewModels;
using WebSite.Common.Utils;
using WebSite.DAL.Db.Models.Repositories;
using WebSite.DAL.Db.Repositories;
using WebSite.Email;
using WebSite.ViewModels;

namespace WebSite.Controllers
{
    [Authorize]
    public class ManagerController : Controller
	{
        public const String MainPage          = "main";
        public const String TrainingPage      = "training";
        public const String CollaborationPage = "collaboration";
        public const String PlacementPage     = "placement";
        public const String SchedulePage      = "schedule";
        public const String ContactPage       = "contact";
        public const String StudentsPage      = "student";
        public const String GalleryPage       = "gallery";

        private IOfflineMessageRepository _offlineMessagesRepository;
        private ICourseGroupRepository    _courseGroupsRepository;
        private ICourseRepository         _courseRepository;
        private ILayoutRepository         _layoutRepository;
        private IMainPageItemsRepository  _mainPageItemsRepository;
        private IEmailRepository          _emailRepository;
        private EmailGateway _emailGateway;

        private IStudentRepository _studentsRepository;


		public class EmailPageViewModel
        {
            public IEnumerable<CourseVM> Courses { get; set; }
        }

        public class AdminPageViewModel
        {
            public AdminPageViewModel(IEnumerable<PageLayoutVM> allLayouts,IEnumerable<CourseVM> courses)
            {
                if (allLayouts == null)
                    return;

                _courses = courses;

                foreach (PageLayoutVM p in allLayouts)
                {
                    if (p.PageName == MainPage)
                        this.MainPageLayout = p;
                    else if (p.PageName == TrainingPage)
                        this.TrainingPageLayout = p;
                    else if (p.PageName == CollaborationPage)
                        this.CollaborationPageLayout = p;
                    else if (p.PageName == PlacementPage)
                        this.PlacementPageLayout = p;
                    else if (p.PageName == SchedulePage)
                        this.SchedulePageLayout = p;
                    else if (p.PageName == ContactPage)
                        this.ContactsPageLayout = p;
                    else if (p.PageName == StudentsPage)
                        this.StudentsPageLayout = p;
                    else if (p.PageName == GalleryPage)
                        this.GalleryPageLayout = p;
                }

                if (this.MainPageLayout == null)
                    this.MainPageLayout = new PageLayoutVM(MainPage);

                if (this.TrainingPageLayout == null)
                    this.TrainingPageLayout = new PageLayoutVM(TrainingPage);

                if (this.CollaborationPageLayout == null)
                    this.CollaborationPageLayout = new PageLayoutVM(CollaborationPage);

                if (this.SchedulePageLayout == null)
                    this.SchedulePageLayout = new PageLayoutVM(SchedulePage);

                if (this.PlacementPageLayout == null)
                    this.PlacementPageLayout = new PageLayoutVM(PlacementPage);

                if (this.ContactsPageLayout == null)
                    this.ContactsPageLayout = new PageLayoutVM(ContactPage);

                if (this.StudentsPageLayout == null)
                    this.StudentsPageLayout = new PageLayoutVM(StudentsPage);

                if (this.GalleryPageLayout == null)
                    this.GalleryPageLayout = new PageLayoutVM(GalleryPage);
            }

            public PageLayoutVM MainPageLayout          { get; private set; }
            public PageLayoutVM TrainingPageLayout      { get; private set; }
            public PageLayoutVM CollaborationPageLayout { get; private set; }
            public PageLayoutVM SchedulePageLayout      { get; private set; }
            public PageLayoutVM PlacementPageLayout     { get; private set; }
            public PageLayoutVM GalleryPageLayout       { get; protected set; }
            public PageLayoutVM ContactsPageLayout      { get; private set; }
            public PageLayoutVM StudentsPageLayout      { get; private set; }

            private IEnumerable<CourseVM> _courses;

            public IEnumerable<SelectListItem> Courses
            {
                get
                {
                    List<SelectListItem> res = new List<SelectListItem>(_courses.Count() + 1);
                    res.Add(new SelectListItem() { Text = "Выберите курс", Value = "-1", Selected = true });

                    foreach (CourseVM c in _courses)
                        res.Add(new SelectListItem() { Text = c.CourseName, Value = c.CourseId.ToString() });

                    return res;
                }
            }
        
        }


        public ManagerController()
            : this(new DbOfflineMessageRepository("HelloWorldDb"), 
              new DbCourseGroupRepository("HelloWorldDb"), 
              new DbCourseRepository("HelloWorldDb"), 
              new DbLayoutRepository("HelloWorldDb"),
              new DbMainPageItemsRepository("HelloWorldDb"),
              new DbEmailRepository("HelloWorldDb"),
              new DbStudentRepository("HelloWorldDb"))
        {

        }

        public ManagerController(IOfflineMessageRepository repository,ICourseGroupRepository groupsRepo,ICourseRepository courseRepo,ILayoutRepository layoutRepo,IMainPageItemsRepository mainPageItemsRepo,IEmailRepository emailRepo,IStudentRepository studentsRepository)
        {
            _offlineMessagesRepository = repository;
            _courseGroupsRepository    = groupsRepo;
            _courseRepository          = courseRepo;
            _layoutRepository          = layoutRepo;
            _mainPageItemsRepository   = mainPageItemsRepo;
            _emailRepository           = emailRepo;
            _studentsRepository        = studentsRepository;


            _emailGateway = new EmailGateway("DbСontext");
        }

        public ActionResult Index()
        {
            AdminPageViewModel model = new AdminPageViewModel(_layoutRepository.LoadAllLayouts(),_courseRepository.LoadPaged(PagedRequest.Everything()).Data);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditStudent(StudentVM student)
        {
            if (ModelState.IsValid)
            {
                _studentsRepository.SaveStudent(student, null);
                return RedirectToAction("EditStudent", new { studentId = student.StudentId });
            }
            else
            {

            }
            return View("EditStudent", student);
        }

        [HttpGet]
        public ActionResult AddIncome()
        {
            EditIncomeVM income = new EditIncomeVM();

            income.Courses.AddRange(_courseRepository.LoadPaged(PagedRequest.Everything()).Data);

            return View(income);
        }

        [HttpGet]
        public ActionResult AddExpense()
        {
            EditExpenseVM expense = new EditExpenseVM();

            expense.Courses.AddRange(_courseRepository.LoadPaged(PagedRequest.Everything()).Data);

            return View(expense);
        }

        [HttpPost]
        public ActionResult DeleteExpense(int expenseId)
        {
            EditExpenseVM expense = _studentsRepository.LoadExpense(expenseId);

            if (expense != null && expense.ScheduledPaymentId.HasValue)
            {
                ScheduledPaymentVM scheduledPayment = _studentsRepository.LoadPayment(expense.ScheduledPaymentId.Value);

                scheduledPayment.Status = ScheduledPaymentStatus.Rescheduled;

                _studentsRepository.SaveScheduledPayment(scheduledPayment);
            }

            _studentsRepository.DeleteExpense(expenseId);

            return Content("OK");
        }

        [HttpPost]
        public ActionResult DeleteIncome(int incomeId)
        {
           EditIncomeVM income = _studentsRepository.LoadIncome(incomeId);

            if (income.ScheduledPaymentId.HasValue)
            {
                ScheduledPaymentVM scheduledPayment = _studentsRepository.LoadPayment(income.ScheduledPaymentId.Value);

                scheduledPayment.Status = ScheduledPaymentStatus.Rescheduled;

                _studentsRepository.SaveScheduledPayment(scheduledPayment);
            }

            _studentsRepository.DeleteIncome(incomeId);

            return Content("OK");
        }

        private void MakePaymentNotification(EditIncomeVM income)
        {
            try
            {
                if (income == null || income.Amount <= 0 || income.PaymentId > 0)
                    return;

                EditExpenseVM expense = income as EditExpenseVM;
                EmailVM email = new EmailVM();

                if (expense != null)
                {
                    email.Subject = "Расходы Hello World";

                    email.Body = String.Format(@"Оповещение о расходе:
                    Дата платежа:{0}
                    Сумма:{1} грн.
                    Тип расходов:{2}
                    Комментарий:{3}", expense.PaymentDate.ToShortDateString(), expense.Amount, EnumHelper<ExpenseType>.GetDisplayValue(expense.ExpenseType), expense.Comment);
                }
                else
                {
                    email.Subject = "Доходы Hello World";

                    email.Body = String.Format(@"Оповещение о поступлении средств:
                    Дата платежа:{0}
                    Сумма:{1} грн.
                    Тип расходов:{2}
                    Комментарий:{3}", income.PaymentDate.ToShortDateString(), income.Amount, EnumHelper<IncomeType>.GetDisplayValue(income.PaymentType), income.Comment);
                }


                email.From     = "info@kharkovitcourses.com";
                email.IsHtml   = false;
                email.SentTime = DateTime.Now;
                email.Status   = EmailStatus.Pending;

                email.Recipients = new List<EmailRecipientVM>();
                EmailRecipientVM to = new EmailRecipientVM();
                to.Recepient = "evgeniy.batov@gmail.com";
                to.To = true;
                EmailRecipientVM to2 = new EmailRecipientVM();
                to2.Recepient = "ann.barantsevich@mail.ru";
                to2.To = true;

                email.Recipients.Add(to);
                email.Recipients.Add(to2);

                _emailRepository.SaveEmail(email);
                _emailGateway.SendAllPending();
            }
            catch (DbEntityValidationException validationError)
            {
                //notification is failed. Add logging later
            }
            catch
            {
                //notification is failed. Add logging later
            }
        }


        [HttpPost]
        public ActionResult AddExpense(EditExpenseVM newExpense)
        {
            if (ModelState.IsValid)
            {
                if (newExpense.ScheduledPaymentId.HasValue && newExpense.ScheduledPaymentId.Value > 0)
                {
                    ScheduledPaymentVM sPayment = _studentsRepository.LoadPayment(newExpense.ScheduledPaymentId.Value);

                    ScheduledPaymentVM nextMonthPayment = sPayment.GenerateNextMonth();

                    //since payment is recurrent and scheule is active new record is needed
                    if (nextMonthPayment != null)
                        _studentsRepository.SaveScheduledPayment(nextMonthPayment);

                    sPayment.Status = ScheduledPaymentStatus.Paid;
                    _studentsRepository.SaveScheduledPayment(sPayment);
                }

                MakePaymentNotification(newExpense);

                _studentsRepository.SaveExpense(newExpense);


                return RedirectToAction("Expenses");
            }
            else
            {
                newExpense.Courses.AddRange(_courseRepository.LoadPaged(PagedRequest.Everything()).Data);

                return View(newExpense);
            }
        }

        [HttpPost]
        public ActionResult AddIncome(EditIncomeVM newIncome)
        {
            if (ModelState.IsValid)
            {
                if (newIncome.PaymentType == IncomeType.StudentPayment)
                {
                    if (!newIncome.ScheduledPaymentId.HasValue || newIncome.ScheduledPaymentId.Value < 1)
                        throw new NotSupportedException("Such operation is not supported");

                    ScheduledPaymentVM payment = _studentsRepository.LoadPayment(newIncome.ScheduledPaymentId.Value);

                    payment.Status = ScheduledPaymentStatus.Paid;

                    _studentsRepository.SaveScheduledPayment(payment);
                }

                MakePaymentNotification(newIncome);

                _studentsRepository.SaveIncome(newIncome);

                return RedirectToAction("Incomes");
            }
            else
            {
                newIncome.Courses.AddRange(_courseRepository.LoadPaged(PagedRequest.Everything()).Data);

                return View(newIncome);
            }
        }

        [HttpGet]
        public ActionResult FindScheduledPayments(int year,int month)
        {
            IEnumerable<ScheduledPaymentVM> scheduledPayments = _studentsRepository.LoadPayments(year, month);

			FindScheduledPaymentsVM vm = new FindScheduledPaymentsVM
			{
				Month = month,
				Year = year,
				Payments = scheduledPayments
			};


			return View("ScheduledPaymentsList", vm);
        }
            

        [HttpGet]
        public ActionResult Incomes(String fromDateFilter,String toDateFilter,int? courseId,int? flowId,int? studentId)
        {
            PagedRequest pr = new PagedRequest(1, 50, "date", "asc");
            DateTime? fromDate = null;
            DateTime? toDate   = null;


            if (!String.IsNullOrEmpty(fromDateFilter))
            {
                fromDate = DateTime.ParseExact(fromDateFilter, "dd/MM/yyyy", CultureInfo.InvariantCulture.DateTimeFormat);
            }

            if (!String.IsNullOrEmpty(toDateFilter))
            {
                toDate = DateTime.ParseExact(toDateFilter, "dd/MM/yyyy", CultureInfo.InvariantCulture.DateTimeFormat);
            }

            IPagedResult<EditIncomeVM> results = _studentsRepository.LoadIncomes(
                fromDate.HasValue ? fromDate.Value : DateTime.Today.AddMonths(-1),
                toDate.HasValue ? toDate.Value :     DateTime.Today,
                courseId, flowId, studentId, pr);

            if (Request.IsAjaxRequest())
            {
                return View("IncomesList", results);
            }
            else
            {
                return View(results);
            }
        }

        [HttpGet]
        public ActionResult Expenses(String fromDateFilter, String toDateFilter, int? courseId, int? flowId, int? studentId)
        {
            PagedRequest pr = new PagedRequest(1, 50, "date", "asc");
            DateTime? fromDate = null;
            DateTime? toDate = null;


            if (!String.IsNullOrEmpty(fromDateFilter))
            {
                fromDate = DateTime.ParseExact(fromDateFilter, "dd/MM/yyyy", CultureInfo.InvariantCulture.DateTimeFormat);
            }

            if (!String.IsNullOrEmpty(toDateFilter))
            {
                toDate = DateTime.ParseExact(toDateFilter, "dd/MM/yyyy", CultureInfo.InvariantCulture.DateTimeFormat);
            }

            IPagedResult<EditExpenseVM> results = _studentsRepository.LoadExpenses(
                fromDate.HasValue ? fromDate.Value : DateTime.Today.AddMonths(-1),
                toDate.HasValue ? toDate.Value : DateTime.Today,
                courseId, flowId, studentId, pr);

            if (Request.IsAjaxRequest())
            {
                return View("ExpensesList", results);
            }
            else
            {
                return View(results);
            }
        }

        [HttpPost]
        public ActionResult SendReminder(ReminderBindingModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ScheduledPaymentVM payment = _studentsRepository.LoadPayment(model.PaymentId);

                    EmailVM email = new EmailVM();

                    email.Subject = model.Subject;  
                    email.Body    = model.Body;
                    email.From    = "info@kharkovitcourses.com";
                    email.IsHtml  = false;
                    email.SentTime = DateTime.Now;
                    email.Status   = EmailStatus.Pending;
                    List<EmailRecipientVM> recipients =  new List<EmailRecipientVM>();
 
                    IEnumerable<EmailRecipientVM> toList = model.Emails == null ? Enumerable.Empty<EmailRecipientVM>() : model.Emails.Split(';').Select(e =>
                    {
                        EmailRecipientVM to = new EmailRecipientVM();
                        to.Recepient = e;
                        to.To = true;
                        return to;
                    });

                    IEnumerable<EmailRecipientVM> bccList = model.BCCs == null ? Enumerable.Empty<EmailRecipientVM>() : model.BCCs.Split(';').Select(e =>
                    {
                        EmailRecipientVM to = new EmailRecipientVM();
                        to.Recepient = e;
                        to.BCC = true;
                        return to;
                    });

                    IEnumerable<EmailRecipientVM> phonesList = model.Phones == null ? Enumerable.Empty<EmailRecipientVM>() : model.Phones.Split(';').Select(e =>
                    {
                        EmailRecipientVM to = new EmailRecipientVM();
                        to.Recepient = e;
                        to.Phone = true;
                        return to;
                    });

                    if (model.Type == ReminderType.Email || model.Type == ReminderType.SMSAndEmail)
                    {
                        recipients.AddRange(toList);
                        recipients.AddRange(bccList);
                    }
                    if (model.Type == ReminderType.SMS || model.Type == ReminderType.SMSAndEmail)
                    {
                        recipients.AddRange(phonesList);
                    }

                    email.Recipients = recipients;
                    _emailRepository.SaveEmail(email);

                    payment.NumberOfSentReminders = payment.NumberOfSentReminders+1;
                    _studentsRepository.SaveScheduledPayment(payment);


                    _emailGateway.SendAllPending();

                    return RedirectToAction("PaymentsPlan");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(String.Empty, e.Message);
                    return View("SendReminder", model);
                }
            }
            else
            {
                return View("SendReminder", model);
            }
        }

        [HttpGet]
        public ActionResult SendReminder(int paymentId)
        {
            ScheduledPaymentVM payment = _studentsRepository.LoadPayment(paymentId);
            StudentVM student          = _studentsRepository.LoadStudent(payment.StudentId);
            CourseFlowVM flow          = _courseRepository.LoadFlow(student.FlowId);
            CourseVM course            = _courseRepository.LoadById(flow.CourseId);
            SignupApplication application = _offlineMessagesRepository.LoadApplicationsByFlow(flow.FlowId, null).FirstOrDefault(a => a.Email.Equals(student.Email));

            EmailTemplateVM template   = _emailRepository.LoadTemplate(EmailTemplate.PaymentNotifiction);

            EmailVM email = template.BuildEmail(Server.MapPath, application, course, flow, student, "", "", "", student.Email, "evgeniy.batov@gmail.com;ann_barantsevich@mail.ru");
            email.Recipients.Add(new EmailRecipientVM() { Recepient = student.Phone1, Phone = true }); 

            return View(new ReminderBindingModel(paymentId, email));
        }

        [HttpGet]
        public ActionResult StudentsByFlow(int flowId)
        {
            IEnumerable<StudentVM> students = _studentsRepository.LoadFlowStudents(flowId);

            return Json(students,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditStudent(int studentId)
        {
            StudentVM student = _studentsRepository.LoadStudent(studentId);

            return View("EditStudent", student);
        }

        [HttpPost]
        public ActionResult AddPayment(int? studentId,int? flowId)
        {
            CourseFlowVM flow = null;
            StudentVM    std  = null;

            if (studentId.HasValue && flowId.HasValue)
            {
                flow = _courseRepository.LoadFlow(flowId.Value);
                std  = _studentsRepository.LoadStudent(studentId.Value);
            }

            ScheduledPaymentVM p = new ScheduledPaymentVM
            {
                Comment = "New payment",
                DueDate = DateTime.Today,
                Flow = flow,
                IsExpense = false,
                IsReccurent = false,
                RecurrentMoment = 0,
                RecurrentType = ScheduledPaymentRecurrent.Unknown,
                ScheduledAmount = 0,
                ScheduledAmountInHryvnas = 0,
                Status = ScheduledPaymentStatus.Unknown,
                StudentName = std == null ? null : std.FullName,
                StudentId = std == null ? 0 : std.StudentId
            };

            p = _studentsRepository.SaveScheduledPayment(p);

            return RedirectToAction("EditScheduledPayment", new { paymentId = p.PaymentId });
        }

        [HttpPost]
        public ActionResult EditScheduledPayment(ScheduledPaymentVM payment)
        {
            if (ModelState.ContainsKey("Flow.FlowId"))
                ModelState["Flow.FlowId"].Errors.Clear();

            if (ModelState.IsValid)
            {
                 _studentsRepository.SaveScheduledPayment(payment);


                return RedirectToAction("EditScheduledPayment", new { paymentId = payment.PaymentId });
            }
            else
            {
                return View("EditScheduledPayment", _studentsRepository.LoadPayment(payment.PaymentId));
            }
        }

        [HttpGet]
        public ActionResult EditScheduledPayment(int paymentId)
        {
            ScheduledPaymentVM payment = _studentsRepository.LoadPayment(paymentId);

            return View("EditScheduledPayment", payment);
        }


        [HttpGet]
        public ActionResult RegisterStudent(int flowId)
        {
            CourseFlowVM flow                          = _courseRepository.LoadFlow(flowId);
            CourseVM course = _courseRepository.LoadById(flow.CourseId);
            IEnumerable<SignupApplication> registrants = _offlineMessagesRepository.LoadApplicationsByFlow(flow.FlowId,null);

            return View(new RegisterStudentVM(course,flow,registrants,null));
        }

        [HttpPost]
        public ActionResult RegisterStudent(RegisterStudentVM newStudentModel)
        {
            if (ModelState.IsValid)
            {
                CourseFlowVM flow = _courseRepository.LoadFlow(newStudentModel.FlowId);

                StudentVM newStudent = new StudentVM
                {
                    Comment = newStudentModel.Comment,
                    Email = newStudentModel.Email,
                    ContractNumber = newStudentModel.ContractNumber,
                    FirstName = newStudentModel.FirstName,
                    FlowId = newStudentModel.FlowId,
                    INN = newStudentModel.INN,
                    LastName = newStudentModel.LastName,
                    MiddleName = newStudentModel.MiddleName,
                    PasspordIssuedDate = newStudentModel.PasspordIssuedDate.HasValue ? newStudentModel.PasspordIssuedDate.Value : DateTime.Today,
                    PassportIssuedAt = "",
                    PassportIssuedBy = newStudentModel.PassportIssuedBy,
                    PassportNo = newStudentModel.PassportNo,
                    PaymentModel = newStudentModel.PaymentModel,
                    Phone1 = newStudentModel.Phone1,
                    Phone2 = newStudentModel.Phone2,
                    StudentSince = flow.ActualStartDate.Value,
                    StudentTill = flow.ActualEndDate.Value
                };


                //generate payments
                IEnumerable<ScheduledPaymentVM> payments = CalculatePayments(newStudentModel.CourseId, newStudentModel.FlowId, newStudentModel.PaymentModel, newStudentModel.FriendBonus, newStudentModel.SecondCourseBonus, newStudent);

                //save new student record
                _studentsRepository.SaveStudent(newStudent, payments);
                
                SignupApplication application = _offlineMessagesRepository.GetApplication(newStudent.Email,newStudent.FlowId);
                if (application != null)
                {
                    //Change application status
                    application.Status = ApplicationStatus.Student;
                    _offlineMessagesRepository.UpdateSignupApplication(application);
                }
                

                return RedirectToAction("ManageCourse", new { flowId = newStudentModel.FlowId });
            }
            else
            {
                newStudentModel.Applications = _offlineMessagesRepository.LoadApplicationsByFlow(newStudentModel.FlowId,ApplicationStatus.Approved);



                return View(newStudentModel);
            }
        }


        [HttpGet]
        public ActionResult ManageCourse(int flowId)
        {
            CourseFlowVM flow                          = _courseRepository.LoadFlow(flowId);
            CourseVM course                            = _courseRepository.LoadById(flow.CourseId);
            IEnumerable<SignupApplication> registrants = _offlineMessagesRepository.LoadApplicationsByFlow(flow.FlowId, ApplicationStatus.Approved);
            IEnumerable<StudentVM> students            = _courseRepository.LoadStudentsByFlow(flowId);


            return View(new ManageCourseVM(course,flow,registrants,students));
        }


        public class UpdateCourseBindingModel
        {   
            public CourseFlowVM Flow { get; set; }
        }

        [HttpPost]
        public ActionResult ManageCourse(UpdateCourseBindingModel updatedModel)
        {
            if (ModelState.IsValid)
            {
                _courseRepository.UpdateFlow(updatedModel.Flow);
            }
            else
            {
            }
            return RedirectToAction("ManageCourse", new { flowId = updatedModel.Flow.FlowId });
        }


        [HttpGet]
        public ActionResult Manage()
        {
            ManageViewModel vm = new ManageViewModel();

            IPagedResult<CourseVM> courses = _courseRepository.LoadPaged(PagedRequest.Everything());

            foreach (CourseVM cVm in courses.Data)
            {
                ICollection<CourseFlowVM> flows = _courseRepository.LoadCourseFlows(cVm.CourseId, null);

                foreach (CourseFlowVM flow in flows)
                {
                    if (flow.Status == FlowStatus.OpenedForRegistration || flow.Status == FlowStatus.Started)
                    {
                        vm.AddFlow(flow, cVm);
                    }
                }
            }

            return View(vm);
        }


        [HttpGet]
        public ActionResult CalcPayments(int courseId, int flowId, String paymentMethod, bool firendBonus, bool loyalBonus)
        {
            IEnumerable<ScheduledPaymentVM> payments = CalculatePayments(courseId, flowId, (StudentPaymentModel)Enum.Parse(typeof(StudentPaymentModel),paymentMethod,true), firendBonus, loyalBonus,null);

            return View("Payments", payments);
        }

        private String Monthes2String(DateTime startDate,int numOfMonths)
        {
            CultureInfo russianCulture = CultureInfo.GetCultureInfo("ru-RU");

            StringBuilder sb = new StringBuilder();

            sb.Append(startDate.ToString("MMMM", russianCulture.DateTimeFormat));

            for (int i = 1; i < numOfMonths; i++)
            {
                sb.Append(',').Append(startDate.AddMonths(i).ToString("MMMM", russianCulture.DateTimeFormat));
            }

            return sb.ToString();
        }

        private IEnumerable<ScheduledPaymentVM> CalculatePayments(int courseId,int flowId, StudentPaymentModel model ,bool firendBonus,bool loyalBonus,StudentVM student)
        {
         
            
            

            IEnumerable<CoursePriceVM> prices =  _courseRepository.LoadCoursePrices(courseId);
            CourseFlowVM flow                 = _courseRepository.LoadFlow(flowId);
            CourseVM course                   = _courseRepository.LoadById(courseId);


            String studentName = student == null ? " " : student.FullName;
            String comment = String.Format("Оплата студента курса {0} {1} за месяц(ы)", course.CourseName, studentName);


            CoursePriceVM price       = null;

            int numberofMonths = course.CourseLengthInMonth(flow.NumberOfLessonsInWeek);
            List <ScheduledPaymentVM> payments = new List<ScheduledPaymentVM>();
            DateTime dueDate = flow.ActualStartDate.Value;

            switch (model)
            {
                case StudentPaymentModel.Monthly:
                    price = prices.OrderByDescending(p => p.Price).First();
                    for (int i = 0; i < numberofMonths;i++,dueDate = dueDate.AddMonths(1))
                    {
                        payments.Add(new ScheduledPaymentVM()
                        {
                            Status = ScheduledPaymentStatus.Scheduled,
                            DueDate = dueDate,
                            ScheduledAmount = Convert.ToInt32(price.Price * 100.00m),
                            Comment = comment + " " + Monthes2String(dueDate, 1)
                        });
                    }
                    break;
                case StudentPaymentModel.SinglePayent:
                    price = prices.OrderBy(p => p.Price).First();
                    payments.Add(new ScheduledPaymentVM()
                    {
                        Status = ScheduledPaymentStatus.Scheduled,
                        DueDate = dueDate,
                        ScheduledAmount = Convert.ToInt32(price.Price * 100.00m * (decimal)numberofMonths),
                        Comment = comment + " " + Monthes2String(dueDate, numberofMonths)
                    });
                    break;
                case StudentPaymentModel.By2Monthes:
                    price               = prices.Count() > 2 ? prices.OrderBy(p => p.Price).ElementAt(2) : prices.OrderBy(p => p.Price).Last();
                    bool oddMonthNumber = numberofMonths % 2 == 0;

                    for (int i = 0; i < numberofMonths; i+=2, dueDate = dueDate.AddMonths(2))
                    {
                        bool lastPayment = i >= numberofMonths - 2;
                        if (lastPayment && !oddMonthNumber)
                        {
                            payments.Add(new ScheduledPaymentVM()
                            {
                                Status = ScheduledPaymentStatus.Scheduled,
                                DueDate = dueDate,
                                ScheduledAmount = Convert.ToInt32(price.Price * 100.00m),
                                Comment = comment + " " + Monthes2String(dueDate, 1)
                            });
                        }
                        else
                        {
                            payments.Add(new ScheduledPaymentVM()
                            {
                                Status = ScheduledPaymentStatus.Scheduled,
                                DueDate = dueDate,
                                ScheduledAmount = Convert.ToInt32(price.Price * 100.00m * 2.0m),
                                Comment = comment + " " + Monthes2String(dueDate, 2)
                            });
                        }
                    }
                    break;
                default:
                    throw new NotSupportedException("Payment method is not suppoerted");
            }

            if (firendBonus)
            {
                decimal discount = price.Price * 20.00m / 100.00m;
                payments.First().ScheduledAmount = payments.First().ScheduledAmount - (int)(discount * 100);
            }
            if (loyalBonus)
            {
                decimal discount = price.Price * 15.00m / 100.00m;
                payments.First().ScheduledAmount = payments.First().ScheduledAmount - (int)(discount * 100);
            }
            return payments; 
        }

        [HttpGet]
        public ActionResult Emails()
        {
            AdminPageViewModel model = new AdminPageViewModel(_layoutRepository.LoadAllLayouts(), _courseRepository.LoadPaged(PagedRequest.Everything()).Data);
            return View(model);
        }

        [HttpGet]
        public ActionResult Courses(ushort page = 0, ushort rows = 0, string sidx = null, string sord = null)
        {
            IPageRequest pageReq = null;

            if (page == 0 && rows == 0 && sidx == null && sord == null)
                pageReq = PagedRequest.Everything();
            else
                pageReq = new PagedRequest(page, rows, sidx, sord);

            IPagedResult<CourseVM> courses = _courseRepository.LoadPaged(pageReq);

            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PaymentsPlan()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CourseGroups(ushort page, ushort rows, string sidx, string sord)
        {
            PagedRequest pageRequest = new PagedRequest(page, rows, sidx, sord);

            IPagedResult<CourseGroupVM> groups = _courseGroupsRepository.LoadPaged(pageRequest);

            return Json(groups, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteGroup(int groupId)
        {
            _courseGroupsRepository.DeleteGroup(groupId);

            return null;
        }

        [HttpPost]
        public ActionResult DeleteCourse(int courseId)
        {
            _courseRepository.DeleteCourse(courseId);

            return null;
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateContent(PageLayoutVM updateLayout)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    updateLayout  = _layoutRepository.UpdateLayout(updateLayout);
                }
                catch
                {
                    ModelState.AddModelError("", "Ошибка обновления сервера. Попробуйте ещё раз");
                }
            }
            else
            {
                ModelState.AddModelError("","Указанные данные не прошли валидацию. Проверьте ввод");
            }
            return PartialView("EditLayout", updateLayout);
        }
        
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePageHeaders(EditPageHeadersVM newHeaders)
        {
            if (ModelState.IsValid)
            {
                if (newHeaders.GroupId.HasValue)
                {
                    _courseGroupsRepository.UpdateHeaders(newHeaders.GroupId.Value,newHeaders);
                }
                else if (newHeaders.CourseId.HasValue)
                {
                    _courseRepository.UpdateHeaders(newHeaders.CourseId.Value, newHeaders);
                }
            }
            else
            {

            }
            return null;
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult UpdateCourseInfo(EditCourseInfoVM courseInfo)
        {
            if (ModelState.IsValid)
            {
                _courseRepository.UpdateCourseInfo(courseInfo);
            }
            else
            {
            }
            return null;
        }
        
        [HttpGet]
        public ActionResult GetCourses()
        {
            var result = _courseRepository.LoadPaged(PagedRequest.Everything());

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCoursePrices(int courseId)
        {
            return Json(_courseRepository.LoadCoursePrices(courseId),JsonRequestBehavior.AllowGet);
            //_courseRepository.LoadCourseFlows
        }

        [HttpPost]
        public ActionResult UpdateCoursePrice(UpdateCourseVM updatedPrice)
        {
            switch (updatedPrice.oper)
            {
                case "add":
                    _courseRepository.CreatePrice(updatedPrice);
                    break;
                case "edit":
                    updatedPrice.PriceId = Convert.ToInt32(updatedPrice.id);
                    _courseRepository.UpdatePrice(updatedPrice);
                    break;
                case "del":
                    _courseRepository.RemovePrice(updatedPrice);
                    break;
            }
            return new HttpStatusCodeResult(200);
        }

        public ActionResult GetCourseInfo(int courseId)
        {
            EditCourseInfoVM result = _courseRepository.LoadCourseInfo(courseId);

            if (result == null)
            {
                result          = new EditCourseInfoVM();
                result.CourseId = courseId;
            }

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateCourse(EditCourseVM updatedCourse)
        {
            if (ModelState.IsValid)
            {
                CourseVM updatedItem = _courseRepository.UpdateCourse(updatedCourse);
            }
            else
            {
            }
            return null;
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateGroup(EditGroupVM updatedGroup)
        {
            if (ModelState.IsValid)
            {
                CourseGroupVM newItem = _courseGroupsRepository.UpdateGroup(updatedGroup);
            }
            else
            {

            }
            return null;
        }

        public ActionResult UpdateApplicationFlow(int appid, int flowid)
        {
            SignupApplication signupApp = _offlineMessagesRepository.GetApplication(appid);

            signupApp.FlowId = flowid;

            _offlineMessagesRepository.UpdateSignupApplication(signupApp);

            return null;
        }

       

        public class SendEmailsBindingModel
        {
            public String To { get; set; }
            public String CC { get; set; }
            public String Subj { get; set; }
            public String Body { get; set; }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet]
        public ActionResult LoadSentEmails(int pageSize = 50, int offset = 0)
        {
            IEnumerable<EmailVM> emails = _emailRepository.LoadEmails(offset, pageSize);

            return View("EmailsList", emails);
        }

        [HttpPost]
        public ActionResult ExcludeStudent(int studentId)
        {
            StudentVM std = _studentsRepository.LoadStudent(studentId);

            if (std != null)
            { 
                _studentsRepository.DeleteStudent(studentId);
            }

            return RedirectToAction("ManageCourse", new { flowID = std.FlowId });
            
        }


        [HttpPost]
        public ActionResult SendEmails(SendEmailsBindingModel model)
        {
            if (!String.IsNullOrEmpty(model.To) && !String.IsNullOrEmpty(model.Subj) && !String.IsNullOrEmpty(model.Body))
            {
                List<String> validatedEMails = new List<string>();
                List<String> validatedCC     = new List<string>();

                String[] emails = model.To.Split(';');
                if (emails != null && emails.Length > 0)
                {
                    foreach(String mail in emails)
                    {
                        if (IsValidEmail(mail))
                            validatedEMails.Add(mail);
                    }
                    if (!String.IsNullOrEmpty(model.CC))
                    {
                        String[] ccc = model.CC.Split(';');
                        if (ccc != null && ccc.Length > 0)
                        {
                            foreach (String c in ccc)
                            {
                                if (IsValidEmail(c))
                                    validatedCC.Add(c);
                            }
                        }
                    }
                    if (validatedEMails.Count > 0)
                    {
                        foreach (String validatedEmail in validatedEMails)
                        {
                            SignupApplication application = _offlineMessagesRepository.GetApplication(validatedEmail);
                            StudentVM          student;
                            CourseVM          course;
                            CourseFlowVM      flow;

                            if (application != null)
                            {
                                course = _courseRepository.LoadById(application.CourseId);
                                flow = _courseRepository.LoadFlow(application.FlowId);
                                student = _courseRepository.LoadStudentsByFlow(application.FlowId).FirstOrDefault(s => s.Email.Equals(application.Email, StringComparison.InvariantCultureIgnoreCase));

                                if (course == null)
                                    course = new CourseVM();

                                if (flow == null)
                                    flow = new CourseFlowVM();

                                if (student == null)
                                    student = new StudentVM();
                            }
                            else
                            {
                                application = new SignupApplication();
                                course      = new CourseVM();
                                flow        = new CourseFlowVM();
                                student     = new StudentVM();
                            }

                            EmailTemplateVM template = new EmailTemplateVM() { Body = model.Body, FIle = "inmemory", Subject = model.Subj, TemplateId = -1 };
                            EmailVM email = template.BuildEmail(null, application, course, flow, student, "","","",validatedEmail,validatedCC.ToArray());
                            email.IsHtml = true;
                            _emailRepository.SaveEmail(email);
                        }
                        try
                        {
                            _emailGateway.SendAllPending();
                        }
                        catch { }
                        return new JsonResult() { Data = new { Message = String.Format("{0} email(s) have been sent", validatedEMails.Count()) } };
                    }
                }
            }
            throw new InvalidOperationException();
        }

        public ActionResult UpdateFlow(CourseFlowVM courseFlow)
        {
            if (ModelState.IsValid)
            {
                if (courseFlow.FlowId > 0)
                    _courseRepository.UpdateFlow(courseFlow);
                else
                    _courseRepository.CreateFlow(courseFlow);
            }
            else
            {

            }
            return null;
        }

        [HttpPost]
        public ActionResult MakePayment(int paymentId,int requestedMonth,int requestedYear)
        {
            ScheduledPaymentVM payment = _studentsRepository.LoadPayment(paymentId);

            if (payment.IsExpense)
            {
                EditExpenseVM newExpense = new EditExpenseVM();
                newExpense.Amount = payment.ScheduledAmountInHryvnas;
                newExpense.Comment = payment.Comment;

                ExpenseType type = ExpenseType.Other;
                if (payment.Comment != null)
                {
                    String lowComment = payment.Comment.ToLower();
                    switch(lowComment)
                    {
                        case "оборудовани":
                            type = ExpenseType.Equipment;
                            break;
                        case "возврат":
                            type = ExpenseType.MoneyBack;
                            break;
                        case "аренд":
                            type = ExpenseType.Rent;
                            break;
                        case "зарплат":
                            type = ExpenseType.Salary;
                            break;
                        case "канцтовар":
                            type = ExpenseType.Stationery; 
                            break;
                    }
                }

                newExpense.ExpenseType        = type;
                newExpense.PaymentDate        = DateTime.Today;
                newExpense.PaymentType        = IncomeType.Other;
                newExpense.RegisteredBy       = "admin";
                newExpense.ScheduledPaymentId = payment.PaymentId;

                MakePaymentNotification(newExpense);

                _studentsRepository.SaveExpense(newExpense);
            }
            else
            {
                EditIncomeVM newIncome = new EditIncomeVM();

                newIncome.Amount = payment.ScheduledAmountInHryvnas;
                newIncome.Comment = payment.Comment;
                newIncome.PaymentDate = DateTime.Today;
                newIncome.ScheduledPaymentId = payment.PaymentId;
                newIncome.PaymentType = payment.StudentId > 0 ? IncomeType.StudentPayment : IncomeType.Other;
                newIncome.RegisteredBy = "admin";

                if (payment.StudentId > 0)
                    newIncome.SelectedStudentId = payment.StudentId;
                if (payment.Flow != null)
                {
                    newIncome.SelectedFlowId = payment.Flow.FlowId;
                    newIncome.SelectedCourseId = payment.Flow.CourseId;
                }

                MakePaymentNotification(newIncome);

                _studentsRepository.SaveIncome(newIncome);
            }

            payment.Status = ScheduledPaymentStatus.Paid;
            _studentsRepository.SaveScheduledPayment(payment);

            return RedirectToAction("PaymentsPlan",new {ReportMonth = requestedMonth, ReportYear=requestedYear});
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PostGroup(CreateGroupVM newGroup)
        {
            if (ModelState.IsValid)
            {
                 CourseGroupVM newItem = _courseGroupsRepository.CreateGroup(newGroup);
            }
            else
            {
              
            }
            return null;
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PostMainPageItem(MainPageItemVM item)
        {
            if (ModelState.IsValid)
            {
                _mainPageItemsRepository.CreateItem(item);
            }
            else
            {
            }
            return null;
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateMainPageItem(MainPageItemVM item)
        {
            if (ModelState.IsValid)
            {
                _mainPageItemsRepository.UpdateItem(item);
            }
            else
            {
            }
            return null;
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteMainPageItem(int itemId)
        {
            _mainPageItemsRepository.DeleteGroup(itemId);
            return Content("OK");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PostCourse(CreateCourseVM newCourse)
        {
            if (ModelState.IsValid)
            {
                CourseVM newItem = _courseRepository.CreateCourse(newCourse);
            }
            else
            {
            }
            return null;
        }

        [HttpGet]
        public ActionResult MainPageItems(ushort page, ushort rows, string sidx, string sord)
        {
            PagedRequest pageRequest = new PagedRequest(page, rows, sidx, sord);

            IPagedResult<MainPageItemVM> messages =  _mainPageItemsRepository.LoadPaged(pageRequest);

            return Json(messages, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult OfflineMessages(ushort page,ushort rows,string sidx,string sord)
        {
            PagedRequest pageRequest = new PagedRequest(page, rows, sidx, sord);

            IPagedResult<OfflineMessage> messages = _offlineMessagesRepository.LoadPaged(pageRequest);

            return Json(messages, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult LoadFeedbacks(ushort page, ushort rows, string sidx, string sord)
        {
            PagedRequest pageRequest = new PagedRequest(page, rows, sidx, sord);

            IPagedResult<FeedbackVM> feedbacks = _courseRepository.LoadFeedbacks(pageRequest);

            return Json(feedbacks, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult UpdateFeedback(FeedbackVM fb)
        {
            FeedbackVM updatedFeedback = _courseRepository.SaveFeedBack(fb);

            return Json(updatedFeedback, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteFeedback(int id)
        {
            _courseRepository.DeleteFeedback(id);

            return new ContentResult() { Content = "OK" };
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
