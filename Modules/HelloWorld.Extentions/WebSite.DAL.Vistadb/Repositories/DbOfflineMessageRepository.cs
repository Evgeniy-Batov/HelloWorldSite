using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Interfaces.Repositories;
using WebSite.DAL.Db.Models;
using WebSite.DAL.Vistadb.Repositories;

namespace WebSite.DAL.Db.Repositories
{
    public class DbOfflineMessageRepository : IOfflineMessageRepository
    {
        private Context _dbContext;

        private SignupApplicationDbModel ViewModel2DbModel(WebSite.Common.Models.ViewModels.SignupApplication vm)
        {
            SignupApplicationDbModel result = new SignupApplicationDbModel();

            result.FlowId         = vm.SelectedGroupId;
            result.AdditionalInfo = vm.Message;
            result.CreatedOn  = vm.PostedOn;
            result.Email      = vm.Email;
            result.FirstName  = vm.Name;
            result.LastName   = vm.LastName;
            result.Status     = (int)vm.Status;
            result.Phone      = vm.Phone;
            result.Refferal   = vm.SelectedReferral;
            result.IP         = vm.Ip;
            result.AccessToken = vm.AccessToken;
            result.ApplicationId = (int)vm.MessageId;

            result.MondayTime = (int)vm.Schedule.GetDesiredTime(DayOfWeek.Monday);
            result.TuesdayTime = (int)vm.Schedule.GetDesiredTime(DayOfWeek.Tuesday);
            result.WednesdayTime = (int)vm.Schedule.GetDesiredTime(DayOfWeek.Wednesday);
            result.ThursdayTime  = (int)vm.Schedule.GetDesiredTime(DayOfWeek.Thursday);
            result.FridayTime = (int)vm.Schedule.GetDesiredTime(DayOfWeek.Friday);
            result.SundayTime = (int)vm.Schedule.GetDesiredTime(DayOfWeek.Sunday);
            result.SaturdayTime = (int)vm.Schedule.GetDesiredTime(DayOfWeek.Saturday);

            return result;
        }

        private WebSite.Common.Models.ViewModels.SignupApplication DbModel2ViewModel(SignupApplicationDbModel vm)
        {
            if (vm == null)
                return null;

            WebSite.Common.Models.ViewModels.SignupApplication result = new WebSite.Common.Models.ViewModels.SignupApplication();

            result.SelectedGroupId = vm.FlowId;
            result.Message         = vm.AdditionalInfo;
            result.PostedOn        = vm.CreatedOn;
            result.Email           = vm.Email;
            result.Name            = vm.FirstName;
            result.LastName        = vm.LastName;
            result.Status          = (WebSite.Common.Models.ViewModels.ApplicationStatus)vm.Status;
            result.Phone           = vm.Phone;
            result.SelectedReferral   = vm.Refferal;
            result.Ip                 = vm.IP;
            result.AccessToken        = vm.AccessToken;
            result.MessageId          = vm.ApplicationId;

            result.FlowId             = vm.FlowId;

            if (vm.Flow != null)
            {
                result.SelectedCourseId = vm.Flow.CourseId;
                result.CourseId         = vm.Flow.CourseId;
            }

            result.Schedule = new Common.Models.ViewModels.ClassesSchedule();
            result.Schedule.SetAvailableTime(DayOfWeek.Monday,(WebSite.Common.Models.ViewModels.LessonsTime)vm.MondayTime);
            result.Schedule.SetAvailableTime(DayOfWeek.Tuesday, (WebSite.Common.Models.ViewModels.LessonsTime)vm.TuesdayTime);
            result.Schedule.SetAvailableTime(DayOfWeek.Wednesday, (WebSite.Common.Models.ViewModels.LessonsTime)vm.WednesdayTime);
            result.Schedule.SetAvailableTime(DayOfWeek.Thursday, (WebSite.Common.Models.ViewModels.LessonsTime)vm.ThursdayTime);
            result.Schedule.SetAvailableTime(DayOfWeek.Friday, (WebSite.Common.Models.ViewModels.LessonsTime)vm.FridayTime);
            result.Schedule.SetAvailableTime(DayOfWeek.Saturday, (WebSite.Common.Models.ViewModels.LessonsTime)vm.SaturdayTime);
            result.Schedule.SetAvailableTime(DayOfWeek.Sunday, (WebSite.Common.Models.ViewModels.LessonsTime)vm.SundayTime);

            return result;
        }


        private OfflineMessage ViewModel2DbModel(Common.Models.ViewModels.OfflineMessage vm)
        {
            OfflineMessage result = new OfflineMessage();
            result.CreatedOn = vm.PostedOn;
            result.Email = vm.Email;
            result.Message = vm.Message;
            result.Name = vm.Name;
            result.Phone = vm.Phone;
            result.Topic = vm.Topic;
            result.IP    = vm.Ip;

            return result;
        }

        private IEnumerable<Common.Models.ViewModels.OfflineMessage> DbModels2ViewModels(IEnumerable<OfflineMessage> dbmodels)
        {
            List<Common.Models.ViewModels.OfflineMessage> result = new List<Common.Models.ViewModels.OfflineMessage>();
            if (dbmodels != null)
            {
                foreach (OfflineMessage m in dbmodels)
                    result.Add(DbModel2ViewModel(m));
            }
            return result;
        }

        private Common.Models.ViewModels.OfflineMessage DbModel2ViewModel(OfflineMessage dbmodel)
        {
            Common.Models.ViewModels.OfflineMessage result = new Common.Models.ViewModels.OfflineMessage();

            result.MessageId = dbmodel.MessageId;
            result.Email = dbmodel.Email;
            result.Message = dbmodel.Message;
            result.Name = dbmodel.Name;
            result.Phone = dbmodel.Phone;
            result.PostedOn = dbmodel.CreatedOn;
            result.Topic = dbmodel.Topic;

            return result;
        }

        public DbOfflineMessageRepository(String connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string should be specified");
            }
            _dbContext = new Context(connectionString);
        }

        public IEnumerable<Common.Models.ViewModels.OfflineMessage> LoadMessages(DateTime? startingFrom)
        {
            if (startingFrom.HasValue)
            {
                return DbModels2ViewModels(_dbContext.OfflineMessages.Where(m => m.CreatedOn > startingFrom.Value).ToList());
            }

            //Non filtered mode
            return DbModels2ViewModels(_dbContext.OfflineMessages.ToList());
        }

        public void UpdateSignupApplication(WebSite.Common.Models.ViewModels.SignupApplication application)
        {
            SignupApplicationDbModel appModel = _dbContext.SignupApplications.FirstOrDefault(a => a.ApplicationId  == application.MessageId);

            FlowDbModel flow = _dbContext.Flows.FirstOrDefault(f => f.FlowId.Equals(application.SelectedGroupId));

            if (appModel != null)
            {
                appModel.ApplyChanges(application, flow);

                if (appModel.Status == (int)(WebSite.Common.Models.ViewModels.ApplicationStatus.Canceled))
                {
                    CanceledSignupApplicationDbModel canceledModel = new CanceledSignupApplicationDbModel(appModel);
                    _dbContext.CanceledSignupApplications.Add(canceledModel);
                    _dbContext.SignupApplications.Remove(appModel);
                }
                _dbContext.SaveChanges();
            }
        }

        public void AddSignupApplication(WebSite.Common.Models.ViewModels.SignupApplication application)
        {
            SignupApplicationDbModel dbModel = ViewModel2DbModel(application);

            _dbContext.SignupApplications.Add(dbModel);
            _dbContext.SaveChanges();

            application.MessageId = dbModel.ApplicationId;
        }

        public IEnumerable<WebSite.Common.Models.ViewModels.SignupApplication> LoadApplicationsByFlow(int flowId, WebSite.Common.Models.ViewModels.ApplicationStatus? statusFilter)
        {
            IEnumerable<SignupApplicationDbModel> rows = null;
            if (statusFilter.HasValue)
            {
                rows = _dbContext.SignupApplications.Where(s=>s.FlowId.Equals(flowId) && s.Status.Equals((int)statusFilter.Value)).ToList();
            }
            else
            {
                rows = _dbContext.SignupApplications.Where(s => s.FlowId.Equals(flowId)).ToList();
            }

            
            List<WebSite.Common.Models.ViewModels.SignupApplication> result = new List<Common.Models.ViewModels.SignupApplication>();
            foreach(SignupApplicationDbModel row in rows)
                result.Add(DbModel2ViewModel(row));

            return result;
        }
        

        public WebSite.Common.Models.ViewModels.SignupApplication LoadApplication(int applicationId, Guid accessKey)
        {
            SignupApplicationDbModel dbModel = _dbContext.SignupApplications.Include("Flow.Course").FirstOrDefault(a => a.ApplicationId.Equals(applicationId) && a.AccessToken.Equals(accessKey));
            
            return DbModel2ViewModel(dbModel);
        }


        public WebSite.Common.Models.ViewModels.SignupApplication GetApplication(int applicationId)
        {
            SignupApplicationDbModel dbModel = _dbContext.SignupApplications.Include("Flow.Course").FirstOrDefault(a => a.ApplicationId.Equals(applicationId));

            return DbModel2ViewModel(dbModel);
        }

        public WebSite.Common.Models.ViewModels.SignupApplication GetApplication(string email)
        {
            SignupApplicationDbModel dbModel = _dbContext.SignupApplications.Include("Flow.Course").FirstOrDefault(a => a.Email.Equals(email));

            return DbModel2ViewModel(dbModel);
        }


        public void AddMessage(Common.Models.ViewModels.OfflineMessage message)
        {
            _dbContext.OfflineMessages.Add(ViewModel2DbModel(message));
            _dbContext.SaveChanges();
        }

        public IPagedResult<Common.Models.ViewModels.OfflineMessage> LoadPaged(IPageRequest pageRequest)
        {
            ulong totalMessages = (uint)_dbContext.OfflineMessages.Count();
            ushort pageCount    = (ushort)(totalMessages / pageRequest.PageSize + 1);
            ushort requestedPage = pageRequest.PageNumber;
            if (requestedPage < pageCount)
                requestedPage = pageCount;

            IEnumerable<OfflineMessage> offlineMessages = null;
            
            if (pageRequest.All)
            {
                offlineMessages = _dbContext.OfflineMessages.ToList();
            }
            else if (pageRequest.SortOrder == "desc")
            {
                offlineMessages = _dbContext.OfflineMessages.OrderByDescending(m => m.MessageId).Skip(pageRequest.PageSize * (pageRequest.PageNumber - 1)).Take(pageRequest.PageSize).ToList();
            }
            else
            {
                offlineMessages = _dbContext.OfflineMessages.OrderBy(m => m.MessageId).Skip(pageRequest.PageSize * (pageRequest.PageNumber - 1)).Take(pageRequest.PageSize).ToList();
            }

            IEnumerable<Common.Models.ViewModels.OfflineMessage> messages = DbModels2ViewModels(offlineMessages);
            
            PagedResult<Common.Models.ViewModels.OfflineMessage> res = new PagedResult<Common.Models.ViewModels.OfflineMessage>(messages,pageRequest.PageSize,pageRequest.PageNumber,pageCount,totalMessages);
            return res;
        }

        public WebSite.Common.Models.ViewModels.SignupApplication GetApplication(string email, int flowid)
        {
            SignupApplicationDbModel dbModel = _dbContext.SignupApplications.Include("Flow.Course").FirstOrDefault(a => a.Email.Equals(email) && a.FlowId.Equals(flowid));

            return DbModel2ViewModel(dbModel);
        }
    }
}
