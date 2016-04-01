using HelloWorld.Extentions.Models;
using HelloWorld.Extentions.Services;
using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.ActionFilter;
using WebSite.Common.Interfaces.Repositories;
using WebSite.Common.Models.ViewModels;
using WebSite.Common.Utils;
using WebSite.DAL.Db.Models.Repositories;
using WebSite.DAL.Db.Repositories;
using WebSite.Email;
using WebSite.Helpers;
using WebSite.Models.BusinessModels;

namespace WebSite.Controllers
{
    public class ContactController : Controller
	{
        private IOfflineMessageRepository _repository;
        private ILayoutRepository         _layoutRepo;
        private ICourseRepository         _courseRepository;
        private IEmailRepository          _emailRepository;

        private EmailGateway              _emailGateway;

		private readonly ICourseService  _courseService;
		private readonly IContentManager _orchardContentManager;

		public ContactController(ICourseService coureService, IContentManager contentManager) :
            this(new DbOfflineMessageRepository("HelloWorldDb"), 
                 new DbLayoutRepository("HelloWorldDb"), 
                 new DbCourseRepository("HelloWorldDb"),
                 new DbEmailRepository("HelloWorldDb")) 
        {
            _emailGateway		   = new EmailGateway("HelloWorldDb");
			_courseService		   = coureService;
			_orchardContentManager = contentManager;
        }


        public ContactController(IOfflineMessageRepository messagesRepo,ILayoutRepository layoutRepo,ICourseRepository courseRepo,IEmailRepository emailRepo)
        {
            _repository = messagesRepo;
            _layoutRepo = layoutRepo;
            _courseRepository = courseRepo;
            _emailRepository = emailRepo;
        }


        [HttpPost]
        public ActionResult LeaveMessage(OfflineMessage message /*, bool captchaValid, string captchaErrorMessage*/)
        {
            if (ModelState.IsValid)
            {
                //if (!captchaValid)
                //{
                //    return Json(new AjaxOperationResult(new InvalidOperationException("Для отправки запроса необходимо ввести контрольные слова указанные на экране")));
                //}

                try
                {
                    message.Ip       = IPResolver.GetClientIpAddress(HttpContext.Request); 
                    message.PostedOn = SmartTime.Now;
                    message.Topic    = Server.HtmlEncode(message.Topic);
                    message.Name     = Server.HtmlEncode(message.Name);
                    message.Message  = Server.HtmlEncode(message.Message);
                         
                    _repository.AddMessage(message);

                    SendEmails(message, EmailTemplate.OfflineQuestionUser, EmailTemplate.OfflineQuestionOwner);

					if (Request.IsAjaxRequest())
					{
						return Json(AjaxOperationResult.Ok);
					}

					MessageViewVM msg = new MessageViewVM();
					msg.Title   = "Сообщение доставлено";
					msg.Message = "Спасибо за проявленный интерес. Ваше сообщение передано. Ответ поступит Вам на почту по мере обработки очереди. Для более быстрой связи используйте телефоны, указанные на сайте.";
					this.TempData["message"] = msg;
					return Redirect("/registrationcomplete");
				}
                catch (Exception e)
                {
					if (Request.IsAjaxRequest())
					{
						return Json(new AjaxOperationResult(e));
					}

					MessageViewVM msg = new MessageViewVM();
					msg.Title = "Ошибка отправки сообщения";
					msg.Message = "К сожалению проихошла ошибка. Попробуйте добавить своё сообщение ещё раз, спасибо!";
					this.TempData["message"] = msg;
					return Redirect("/registrationcomplete");
				}
            }
            
            return Json(new AjaxOperationResult(ModelState));
        }

        [HttpGet]
        public ActionResult Message()
        {
            MessageViewVM vm = this.TempData["message"] as MessageViewVM;

            if (vm == null)
            {
                vm = MessageViewVM.NoMessage;
            }

            return View(vm);
        }

        [HttpGet]
        public ActionResult Index()
        {
            PageLayoutVM layout = _layoutRepo.LoadLayout("contact");

            return View(layout);
        }

        [HttpGet]
        [Authorize]
        public ActionResult ApplicationsByFlow(int courseId, int flowId)
        {
            if (flowId == -1)
            {
                List<SignupApplication> apps = new List<SignupApplication>();

                foreach (CourseFlowVM courseFlow in _courseRepository.LoadCourseFlows(courseId, null))
                {
                    apps.AddRange(_repository.LoadApplicationsByFlow(courseFlow.FlowId, null));
                }
                return Json(apps.AsEnumerable(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(_repository.LoadApplicationsByFlow(flowId, null), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult FlowsByCourse(int courseId,bool? withHistory)
        {
            ICollection<CourseFlowVM> flows = null;

            if (withHistory.HasValue && withHistory.Value)
            {
                flows = _courseRepository.LoadCourseFlows(courseId,null);
            }
            else
            {
                flows = _courseRepository.LoadCourseFlows(courseId, FlowStatus.OpenedForRegistration);
            }

             if (flows.Count == 0)
             {
                 CourseFlowVM newFlow = CourseFlowVM.CreateDefault(courseId);
                 _courseRepository.CreateFlow(newFlow);
                 return FlowsByCourse(courseId,null);
             }

             return Json(flows,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            IPagedResult<CourseVM> courses       = _courseRepository.LoadPaged(PagedRequest.Everything());

            SignupApplication defaultApplication = new SignupApplication();

            SignupApplicationData data           = new SignupApplicationData(courses.Data.Where(c=>c.Order > 0).OrderBy(c=>c.Order) 
                , defaultApplication);

            return View(data);
        }

        private void SendEmails(IEnumerable<SignupApplication> apps, EmailTemplate userTemplate,EmailTemplate ownerTemplate)
        {
            foreach (SignupApplication app in apps)
                SendEmails(app, userTemplate, ownerTemplate);
        }

        private void SendEmails(OfflineMessage message, EmailTemplate usersTemplate, EmailTemplate ownersTemplate)
        {
            try
            {
                EmailTemplateVM userTemplate  = _emailRepository.LoadTemplate(usersTemplate);
                EmailTemplateVM ownerTemplate = _emailRepository.LoadTemplate(ownersTemplate);

                if (userTemplate != null)
                {
                    EmailVM userEmail = userTemplate.BuildEmail(Server.MapPath, message,message.Email);
                    if (userEmail != null)
                        _emailRepository.SaveEmail(userEmail);
                }
                if (ownerTemplate != null)
                {
                    EmailVM ownerEmail = ownerTemplate.BuildEmail(Server.MapPath,message,"ann_barantsevich@mail.ru", "evgeniy.batov@gmail.com");
                    if (ownerEmail != null)
                        _emailRepository.SaveEmail(ownerEmail);

                }
                _emailGateway.SendAllPending();
            }
            catch
            {
            }
        }

        private void SendEmails(SignupApplication app,EmailTemplate usersTemplate,EmailTemplate ownersTemplate)
        {
            try
            {
                CourseVM course = _courseRepository.LoadById(app.SelectedCourseId);
                CourseFlowVM flow = _courseRepository.LoadFlow(app.SelectedGroupId);
                StudentVM student = _courseRepository.LoadStudentsByFlow(app.FlowId).FirstOrDefault(s => s.Email.Equals(app.Email, StringComparison.InvariantCultureIgnoreCase));


                EmailTemplateVM userTemplate = _emailRepository.LoadTemplate(usersTemplate);
                EmailTemplateVM ownerTemplate = _emailRepository.LoadTemplate(ownersTemplate);

                if (userTemplate != null)
                {
                    EmailVM userEmail = userTemplate.BuildEmail(Server.MapPath, app, course, flow, student,GetEditUrl(app), GetConfirmUrl(app), GetCancelUrl(app), app.Email);
                    if (userEmail != null)
                        _emailRepository.SaveEmail(userEmail);
                }
                if (ownerTemplate != null)
                {
                    EmailVM ownerEmail = ownerTemplate.BuildEmail(Server.MapPath, app, course, flow, student,  GetEditUrl(app), GetConfirmUrl(app), GetCancelUrl(app), "ann_barantsevich@mail.ru", "evgeniy.batov@gmail.com");
                    if (ownerEmail != null)
                        _emailRepository.SaveEmail(ownerEmail);

                }
                _emailGateway.SendAllPending();
            }
            catch
            {
            }
        }


        [HttpPost]
        public ActionResult ConfirmRegistration(int appId, Guid accessToken)
        {
            SignupApplication app = _repository.LoadApplication(appId, accessToken);
            if (app == null)
                return HttpNotFound();

            app.Status = ApplicationStatus.Approved;
            _repository.UpdateSignupApplication(app);

            SendEmails(app, EmailTemplate.ConfirmationUser, EmailTemplate.ConfirmationOwners);

            IEnumerable<SignupApplication> remindersList = GetReminderWaitingApps(app);

            List<SignupApplication> antiSpamList = new List<SignupApplication>();

            foreach (SignupApplication sa in remindersList)
            {
                EmailVM lastSentEmail = _emailRepository.LoadEmails(sa.Email,1).FirstOrDefault();

                if (lastSentEmail != null && lastSentEmail.SentTime.HasValue && (DateTime.Now - lastSentEmail.SentTime.Value).TotalDays < 7.0)
                    continue;

                antiSpamList.Add(sa);
            }

            SendEmails(antiSpamList, EmailTemplate.ReminderUser, EmailTemplate.ReminderOwners);

			if (Request.IsAjaxRequest())
			{
				return new EmptyResult();
			}
			else
			{
				MessageViewVM msg = new MessageViewVM();
				msg.Title   = "Регистрация окончена";
				msg.Message = "Подтверждение Вашей регистрации прошло успешно. Мы отправили письмо с деталями Вам на почту."; 

				this.TempData["message"] = msg;

				return Redirect("/registrationcomplete");
			}
        }

        [HttpPost]
        public ActionResult CancelRegistration(int appId, Guid accessToken)
        {
            SignupApplication app = _repository.LoadApplication(appId, accessToken);
            if (app == null)
                return HttpNotFound();

            app.Status = ApplicationStatus.Canceled;
            _repository.UpdateSignupApplication(app);
            SendEmails(app, EmailTemplate.CancelationUser, EmailTemplate.CancelationOwners);

			if (Request.IsAjaxRequest())
			{
				return new EmptyResult();
			}
			else
			{
				MessageViewVM msg = new MessageViewVM();
				msg.Title   = "Регистрация отменена";
				msg.Message = "Ваша анкета удалена безвозвратно.";

				this.TempData["message"] = msg;

				return Redirect("/registrationcomplete");
			}
        }


        [HttpPost]
        public ActionResult EditSignUp([Bind(Prefix = "Application")] SignupApplication message, [Bind(Prefix = "Schedule")] ClassesSchedule schedule)
        {
            IPagedResult<CourseVM> courses = _courseRepository.LoadPaged(PagedRequest.Everything());
            SignupApplicationData data = new SignupApplicationData(courses.Data, message);

            data.SetPrelopadedGroups(_courseRepository.LoadCourseFlows(message.SelectedCourseId,FlowStatus.OpenedForRegistration));

           
            //Post message
            message.Schedule = schedule;
            message.Ip       = IPResolver.GetClientIpAddress(HttpContext.Request);

            _repository.UpdateSignupApplication(message);

            SendEmails(message, EmailTemplate.UpdateUser, EmailTemplate.UpdateOwners);


            return View("EditApplication", data);
        }

        [HttpGet]
        public ActionResult EditApplication(int appId, String accessT, String operation)
        {
            Guid token = GuidHelper.Base64ToGuid(accessT);

            SignupApplication application = _repository.LoadApplication(appId, token);

            if (application == null)
            {

                MessageViewVM vm = new MessageViewVM();
                vm.Title   = "Анекта не найдена";
                vm.Message = "Запрошенная анкета абитуриента не найдена. За более детальной информацией обращайтесь по телефонам указанным на сайте.";
                this.TempData["message"] = vm;
                //not found or key doesn't match
                return RedirectToAction("Message");
            }
            else
            {
                IPagedResult<CourseVM> courses = _courseRepository.LoadPaged(PagedRequest.Everything());

                SignupApplicationData data = new SignupApplicationData(courses.Data, application);

                data.SetPrelopadedGroups(_courseRepository.LoadCourseFlows(application.CourseId,null));

                return View(data);
            }
           
        }

        private IEnumerable<SignupApplication> GetReminderWaitingApps(SignupApplication app)
        {
            IEnumerable<SignupApplication> confirmedApps 
                = _repository.LoadApplicationsByFlow(app.SelectedGroupId, ApplicationStatus.Approved);

            return confirmedApps.Where(a => a.MessageId != app.MessageId).AsEnumerable();
        }

		private string RootUrl
		{
			get { return string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")); }
		}

        private String GetConfirmUrl(SignupApplication message)
        {
			return String.Format("{3}/registrationcomplete?appId={0}&accessT={1}&operation={2}", message.MessageId, Server.UrlEncode(message.AccessToken.ToString()),"Confirm", RootUrl);

            //return Url.Action("EditApplication", "Contact", new { appId = message.MessageId, accessT = GuidHelper.GuidToBase64(message.AccessToken), operation = "Confirm" }, Request.Url.Scheme);
        }

        private String GetCancelUrl(SignupApplication message)
        {
			return String.Format("{3}/registrationcomplete?appId={0}&accessT={1}&operation={2}", message.MessageId, Server.UrlEncode(message.AccessToken.ToString()), "Cancel", RootUrl);

			//return Url.Action("EditApplication", "Contact", new { appId = message.MessageId, accessT = GuidHelper.GuidToBase64(message.AccessToken), operation = "Cancel" }, Request.Url.Scheme);
        }

        private String GetEditUrl(SignupApplication message)
        {
			return String.Format("{3}/registrationcomplete?appId={0}&accessT={1}&operation={2}", message.MessageId, Server.UrlEncode(message.AccessToken.ToString()), "Contact", RootUrl);

			//return Url.Action("EditApplication", "Contact", new { appId = message.MessageId, accessT = GuidHelper.GuidToBase64(message.AccessToken)}, Request.Url.Scheme);
        }


        [HttpPost]
        public ActionResult SignUp(SignupApplication application)
        {
			var course = _orchardContentManager.Get<ContentPart>(application.CourseId);

			if (course == null)
				throw new ArgumentOutOfRangeException("course");

			int oldSystemCourseId = Convert.ToInt32(((dynamic)course.ContentItem).CoursePage.OldDbCourseId.Value);

			//load and find the closest active flow 

			ICollection<CourseFlowVM> flows = _courseRepository.LoadCourseFlows(oldSystemCourseId, FlowStatus.OpenedForRegistration);

			int flowId = flows.OrderBy(f => f.EstimatedStartDate).Last().FlowId;

			//view model must be adjusted to proceed with registration properly
			application.CourseId		 = oldSystemCourseId;
			application.SelectedCourseId = oldSystemCourseId;
			application.SelectedGroupId  = flowId;
			application.SelectedReferral = "Поиск в интернете";

			IPagedResult<CourseVM> courses = _courseRepository.LoadPaged(PagedRequest.Everything());

			SignupApplicationData data     = new SignupApplicationData(courses.Data, application);

            data.SetPrelopadedGroups(_courseRepository.LoadCourseFlows(application.SelectedCourseId,FlowStatus.OpenedForRegistration));

			//if (captchaValid.HasValue && ModelState.IsValid)
			//{
			//Post message
				application.Schedule = new ClassesSchedule(); //schedule;
                application.Ip          = IPResolver.GetClientIpAddress(HttpContext.Request);
                application.PostedOn    = SmartTime.Now;
                application.Status      = ApplicationStatus.Sent;
                application.AccessToken = Guid.NewGuid();

                _repository.AddSignupApplication(application);

                SendEmails(application, EmailTemplate.SignupUser, EmailTemplate.SignupOwner);

                MessageViewVM msg = new MessageViewVM();
                msg.Title         = "Подтверждение регистрации";
                msg.Message       = String.Format(@"Поздравляем, Вы успешно прошли первый этап регистрации. Для подтверждения регистрации 
                    Вам необходимо перейти по ссылке, которая отправлена на указанный Вами e-mail {0}.",application.Email);

                this.TempData["message"] = msg;

                return Redirect("/registrationcomplete");
            //}
            //else if (!captchaValid)
            //{
            //    data.Application.Schedule = schedule;
            //    ModelState.AddModelError("recaptcha_response_field", "Контрольные слова введены неверно");
            //}
            
            return View(data);
        }

		private void GetCourses()
		{
			throw new NotImplementedException();
		}
	}
}
