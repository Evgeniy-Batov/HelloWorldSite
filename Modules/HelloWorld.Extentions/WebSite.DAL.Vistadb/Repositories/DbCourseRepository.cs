using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Interfaces.Repositories;
using WebSite.Common.Models.ViewModels;
using WebSite.DAL.Db.Models;
using WebSite.DAL.Vistadb.Repositories;
using WebSite.DAL.Db.Models.Models;
using System.Data.Entity;
using System.Globalization;
using WebSite.DAL.Db.Models.Repositories;

namespace WebSite.DAL.Db.Repositories
{
    public class DbCourseRepository:ICourseRepository
    {
        private Context _dbContext;

        public DbCourseRepository(String connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentOutOfRangeException("connectionString");
            }
            _dbContext = new Context(connectionString);
        }



        public static IEnumerable<CourseVM> DbModel2ViewMode(IEnumerable<CourseDbM> courses)
        {
            List<CourseVM> result = new List<CourseVM>();
            if (courses != null)
            {
                foreach (CourseDbM c in courses)
                {
                    result.Add(DbModel2ViewMode(c));
                }
            }
            return result;
        }

        public static EditCourseInfoVM DbModel2ViewModel(CourseInfoDbM courseInfo)
        {
            if (courseInfo == null)
                return null;

            EditCourseInfoVM result = new EditCourseInfoVM();
            result.ActionHtml = courseInfo.ActionHtml;
            result.CourseId = courseInfo.CourseId;
            result.ExtraJS = courseInfo.ExtraJS;
            result.LengthHtml = courseInfo.LengthHtml;
            result.NewsHtml = courseInfo.NewsHtml;
            result.PriceHtml = courseInfo.PriceHtml;
            result.RenderAction = courseInfo.RenderAction;
            result.RenderFB = courseInfo.RenderFB;
            result.RenderLength = courseInfo.RenderLength;
            result.RenderNews = courseInfo.RenderNews;
            result.RenderOK = courseInfo.RenderOK;
            result.RenderPrice = courseInfo.RenderPrice;
            result.RenderSchedule = courseInfo.RenderSchedule;
            result.RenderSignUp = courseInfo.RenderSignUp;
            result.RenderSyllabus = courseInfo.RenderSyllabus;
            result.RenderVK = courseInfo.RenderVK;
            result.ScheduleHtml = courseInfo.ScheduleHtml;
            result.SignUpHtml = courseInfo.SignUpHtml;
            result.SyllabusHtml = courseInfo.SyllabusHtml;

            return result;
        }

        public static FeedbackVM DbModel2ViewModel(FeedbackDbModel dbModel)
        {
            FeedbackVM result   = new FeedbackVM();
            result.CourseId     = dbModel.CourseId;
            result.FBProfileRef = dbModel.FBProfileRef;
            result.FeedBack     = dbModel.FeedBack;
            result.Id           = dbModel.Id;
            result.ImageRef     = dbModel.ImageRef;
            result.OKProfileRef = dbModel.OKProfileRef;
            result.PostTime     = dbModel.PostTime;
            result.StudentName  = dbModel.StudentName;
            result.VKProfileRef = dbModel.VKProfileRef;

            if (dbModel.Course != null)
            {
                result.CourseName = dbModel.Course.CourseName;
                result.CourseRef = dbModel.Course.Route;
            }
            return result;
        }

        public static IEnumerable<FeedbackVM> DbModel2ViewModel(IEnumerable<FeedbackDbModel> dbModels)
        {
            if (dbModels == null)
                return null;

            List<FeedbackVM> result = new List<FeedbackVM>(dbModels.Count());
            foreach (FeedbackDbModel model in dbModels)
                result.Add(DbModel2ViewModel(model));

            return result;
        }

        public static CoursePriceVM DbModel2ViewModel(CoursePriceDbM dbModel)
        {
            CoursePriceVM result = new CoursePriceVM();
            result.Condition = dbModel.Conditional;
            result.CourseId = dbModel.CourseId;
            result.CustomCSS = dbModel.CustomCSS;
            result.Price = dbModel.Price;
            result.PriceId = dbModel.PriceId;
            result.ShortCondition = dbModel.ShortConditional;
            result.ValidTill = dbModel.ValidTill;

            return result;
        }

        public static IEnumerable<CoursePriceVM> DbModel2ViewModel(IEnumerable<CoursePriceDbM> dbModels)
        {
            if (dbModels == null)
                return null;

            List<CoursePriceVM> res = new List<CoursePriceVM>(dbModels.Count());
            foreach (CoursePriceDbM price in dbModels)
                res.Add(DbModel2ViewModel(price));

            return res.AsEnumerable();
        }

        public static CourseFlowVM DbModel2ViewModel(FlowDbModel flow)
        {
            CourseFlowVM vm = new CourseFlowVM();
            vm.ActualEndDate = flow.ActualEndDate;
            vm.ActualStartDate = flow.ActualStartDate;
            vm.CourseId = flow.CourseId;
            vm.CustomName = flow.CustomName;
            vm.EstimatedStartDate = flow.EstimatedStartDate;
            vm.FlowId = flow.FlowId;
            vm.Status = (FlowStatus)flow.Status;
            vm.StartDateStr = flow.StartDateStr;

            vm.MondayStart = Minutes2String(flow.MondayStart);
            vm.MondayEnd = Minutes2String(flow.MondayEnd);
            vm.ThuesdayStart = Minutes2String(flow.ThuesdayStart);
            vm.ThuesdayEnd = Minutes2String(flow.ThuesdayEnd);
            vm.WednesdayStart = Minutes2String(flow.WednesdayStart);
            vm.WednesdayEnd = Minutes2String(flow.WednesdayEnd);
            vm.ThursdayStart = Minutes2String(flow.ThursdayStart);
            vm.ThursdayEnd = Minutes2String(flow.ThursdayEnd);
            vm.FridayStart = Minutes2String(flow.FridayStart);
            vm.FridayEnd = Minutes2String(flow.FridayEnd);
            vm.SaturdayStart = Minutes2String(flow.SaturdayStart);
            vm.SaturdayEnd = Minutes2String(flow.SaturdayEnd);
            vm.SundayStart = Minutes2String(flow.SundayStart);
            vm.SundayEnd = Minutes2String(flow.SundayEnd);

            return vm;
        }

        private static String Minutes2String(int minutes)
        {
            TimeSpan span = new TimeSpan(0, minutes, 0);

            return span.ToString(@"hh\:mm");
        }

        public static FlowDbModel ViewModel2DbModel(CourseFlowVM flow)
        {
            FlowDbModel dm = new FlowDbModel();
            dm.ActualEndDate = flow.ActualEndDate;
            dm.ActualStartDate = flow.ActualStartDate;
            dm.CourseId = flow.CourseId;
            dm.CustomName = flow.CustomName;
            dm.EstimatedStartDate = flow.EstimatedStartDate;
            dm.StartDateStr = flow.StartDateStr;
            dm.FlowId = flow.FlowId;
            dm.Status = (int)flow.Status;

            return dm;
        }

        public static CourseVM DbModel2ViewMode(CourseDbM course)
        {
            return DbModel2ViewMode(course, false, false);
        }

        public static CourseVM DbModel2ViewMode(CourseDbM course,bool convertGroup,bool convertInfo)
        {
            if (course == null)
                return null;

            CourseVM result = new CourseVM();
            result.CourseName = course.CourseName;
            result.Breadcrumb = course.Breadcrumb;
            result.CourseGroupId = course.CourseGroupId;
            if (course.Group != null)
                result.CourseGroupName = course.Group.GroupName;

            result.CourseId = course.CourseId;
            result.CourseImageRef = course.CourseImageRef;
            result.Description = course.Description;
            result.HowToAchieve = course.HowToAchieve;
            result.KeyWords = course.KeyWords;
            result.Order = course.Order;
            result.PageTitle = course.PageTitle;
            result.PricePerMonth = course.PricePerMonth;
            result.Robots = course.Robots;
            result.Route = course.Route;
            result.TitleH1 = course.TitleH1;
            result.WhatForHtml = course.WhatForHtml;
            result.WhatIsItHtml = course.WhatIsItHtml;
            result.WhatIsRequired = course.WhatIsRequired;
            result.WhoRequresIt = course.WhoRequresIt;

            result.NextCourse     = course.NextCourse;
            result.PreviousCourse = course.PreviousCourse;

            result.CustomPageHtml = course.CustomPageHtml;
            result.MenuItemStyle  = course.MenuItemStyle;
            result.IsNew          = course.IsNew.HasValue ? course.IsNew.Value : false;

            if (convertGroup && course.Group != null)
                result.Group = DbCourseGroupRepository.DbModel2ViewMode(course.Group);

            if (convertInfo && course.CourseInfo != null)
                result.Info = DbModel2ViewModel(course.CourseInfo);

            List<CoursePriceVM> prices = new List<CoursePriceVM>();
            if (course.AdditionalPrices != null)
            {
                foreach (CoursePriceDbM p in course.AdditionalPrices.OrderBy(p=>p.Price))
                    prices.Add(DbModel2ViewModel(p));    
            }
            result.Prices = prices;
            return result;
        }


        public IPagedResult<CourseVM> LoadPaged(IPageRequest pageRequest)
        {
            ulong totalMessages = (uint)_dbContext.Courses.Count();
            ushort pageCount = (ushort)(totalMessages / pageRequest.PageSize + 1);
            ushort requestedPage = pageRequest.PageNumber;
            if (requestedPage < pageCount)
                requestedPage = pageCount;

            IEnumerable<CourseDbM> courses = null;

            if (pageRequest.All)
            {
                courses = _dbContext.Courses.ToList();
            }
            else if (pageRequest.SortOrder == "desc")
            {
                courses = _dbContext.Courses.OrderByDescending(m => m.CourseId).Skip(pageRequest.PageSize * (pageRequest.PageNumber - 1)).Take(pageRequest.PageSize).ToList();
            }
            else
            {
                courses = _dbContext.Courses.OrderBy(m => m.CourseId).Skip(pageRequest.PageSize * (pageRequest.PageNumber - 1)).Take(pageRequest.PageSize).ToList();
            }

            return new PagedResult<CourseVM>(DbModel2ViewMode(courses), pageRequest.PageSize, pageRequest.PageNumber, pageCount, totalMessages);
        }


        public CourseVM CreateCourse(CreateCourseVM createVM)
        {
            CourseDbM newCourse = new CourseDbM(createVM);

            _dbContext.Courses.Add(newCourse);

            _dbContext.SaveChanges();

            return DbModel2ViewMode(newCourse);
        }


        public CourseVM UpdateCourse(EditCourseVM editVM)
        {
            CourseDbM course = _dbContext.Courses.FirstOrDefault(c => c.CourseId == editVM.CourseId);
            if (course != null)
            {
                course.ApplyChanges(editVM);
                _dbContext.SaveChanges();
            }
            return DbModel2ViewMode(course);
        }

        public CourseVM LoadByPath(String path)
        {
            return DbModel2ViewMode(_dbContext.Courses.Include("Group").Include("CourseInfo").FirstOrDefault(c => c.Route.Equals(path)),true,true);
        }

        public CourseVM LoadById(int id)
        {
            return DbModel2ViewMode(GetById(id));
        }

        public CourseFlowVM CreateFlow(CourseFlowVM newFlow)
        {
            _dbContext.Flows.Add(ViewModel2DbModel(newFlow));
            _dbContext.SaveChanges();
            return newFlow;
        }

        public CourseFlowVM UpdateFlow(CourseFlowVM updatedFlow)
        {
            FlowDbModel flowRecord = _dbContext.Flows.FirstOrDefault(f => f.FlowId.Equals(updatedFlow.FlowId));
            if (flowRecord != null)
            {
                flowRecord.ActualEndDate = updatedFlow.ActualEndDate;
                flowRecord.ActualStartDate = updatedFlow.ActualStartDate;
                flowRecord.CustomName = updatedFlow.CustomName;
                flowRecord.EstimatedStartDate = updatedFlow.EstimatedStartDate;
                flowRecord.StartDateStr = updatedFlow.StartDateStr;
                flowRecord.Status       = (int)updatedFlow.Status;

                if (updatedFlow.MondayStart != null)
                    flowRecord.MondayStart = (int)DateTime.ParseExact(updatedFlow.MondayStart, @"HH\:mm", CultureInfo.InvariantCulture).TimeOfDay.TotalMinutes;

                if (updatedFlow.MondayEnd != null)
                    flowRecord.MondayEnd   = (int)DateTime.ParseExact(updatedFlow.MondayEnd, @"HH\:mm", CultureInfo.InvariantCulture).TimeOfDay.TotalMinutes;

                if (updatedFlow.ThuesdayStart != null)
                    flowRecord.ThuesdayStart = (int)DateTime.ParseExact(updatedFlow.ThuesdayStart, @"HH\:mm", CultureInfo.InvariantCulture).TimeOfDay.TotalMinutes;

                if (updatedFlow.ThuesdayEnd != null)
                    flowRecord.ThuesdayEnd   = (int)DateTime.ParseExact(updatedFlow.ThuesdayEnd, @"HH\:mm", CultureInfo.InvariantCulture).TimeOfDay.TotalMinutes;

                if (updatedFlow.WednesdayStart != null)
                    flowRecord.WednesdayStart = (int)DateTime.ParseExact(updatedFlow.WednesdayStart, @"HH\:mm", CultureInfo.InvariantCulture).TimeOfDay.TotalMinutes;

                if (updatedFlow.WednesdayEnd != null)
                    flowRecord.WednesdayEnd = (int)DateTime.ParseExact(updatedFlow.WednesdayEnd, @"HH\:mm", CultureInfo.InvariantCulture).TimeOfDay.TotalMinutes;

                if (updatedFlow.ThursdayStart != null)
                    flowRecord.ThursdayStart = (int)DateTime.ParseExact(updatedFlow.ThursdayStart, @"HH\:mm", CultureInfo.InvariantCulture).TimeOfDay.TotalMinutes;

                if (updatedFlow.ThursdayEnd != null)
                    flowRecord.ThursdayEnd = (int)DateTime.ParseExact(updatedFlow.ThursdayEnd, @"HH\:mm", CultureInfo.InvariantCulture).TimeOfDay.TotalMinutes;

                if (updatedFlow.FridayStart != null)
                    flowRecord.FridayStart = (int)DateTime.ParseExact(updatedFlow.FridayStart, @"HH\:mm", CultureInfo.InvariantCulture).TimeOfDay.TotalMinutes;

                if (updatedFlow.FridayEnd != null)
                    flowRecord.FridayEnd = (int)DateTime.ParseExact(updatedFlow.FridayEnd, @"HH\:mm", CultureInfo.InvariantCulture).TimeOfDay.TotalMinutes;

                if (updatedFlow.SaturdayStart != null)
                    flowRecord.SaturdayStart= (int)DateTime.ParseExact(updatedFlow.SaturdayStart, @"HH\:mm", CultureInfo.InvariantCulture).TimeOfDay.TotalMinutes;

                if (updatedFlow.SaturdayEnd != null)
                    flowRecord.SaturdayEnd = (int)DateTime.ParseExact(updatedFlow.SaturdayEnd, @"HH\:mm", CultureInfo.InvariantCulture).TimeOfDay.TotalMinutes;

                if (updatedFlow.SundayStart != null)
                    flowRecord.SundayStart = (int)DateTime.ParseExact(updatedFlow.SundayStart, @"HH\:mm", CultureInfo.InvariantCulture).TimeOfDay.TotalMinutes;

                if (updatedFlow.SundayEnd != null)
                    flowRecord.SundayEnd = (int)DateTime.ParseExact(updatedFlow.SundayEnd, @"HH\:mm", CultureInfo.InvariantCulture).TimeOfDay.TotalMinutes;
            }
            _dbContext.SaveChanges();
            return DbModel2ViewModel(flowRecord);
        }

        public CourseFlowVM LoadFlow(int flowId)
        {
            return DbModel2ViewModel(_dbContext.Flows.FirstOrDefault(f => f.FlowId.Equals(flowId)));
        }

        public ICollection<CourseFlowVM> LoadCourseFlows(int courseId, FlowStatus? status)
        {
            List<FlowDbModel> models;

            if (status.HasValue)
            {
                int statusFilter = (int)status.Value;

                models = _dbContext.Flows.Where(f => f.CourseId.Equals(courseId) && f.Status.Equals(statusFilter)).ToList();
            }
            else
            {
                models = _dbContext.Flows.Where(f => f.CourseId.Equals(courseId)).ToList();
            }
            List<CourseFlowVM> result = new List<CourseFlowVM>(models.Count);
            foreach (FlowDbModel flow in models)
            {
                result.Add(DbModel2ViewModel(flow));
            }

            return result;
        }

        public CourseDbM GetById(int courseId)
        {
            return _dbContext.Courses.FirstOrDefault(c => c.CourseId == courseId);
        }

        public void DeleteCourse(int courseId)
        {
            CourseDbM course = this.GetById(courseId);
            if (course != null)
            {
                _dbContext.Courses.Remove(course);
                _dbContext.SaveChanges();
            }
        }

        public CourseVM UpdateHeaders(int courseId, EditPageHeadersVM newHeaders)
        {
            CourseDbM course = GetById(courseId);
            if (course != null)
            {
                course.ModifyHeaders(newHeaders);
                _dbContext.SaveChanges();
            }
            return DbModel2ViewMode(course);
        }


        public CourseVM UpdateCourseInfo(EditCourseInfoVM editInfo)
        {
            CourseDbM course = GetById(editInfo.CourseId);

            if (course.CourseInfo == null)
            {
                _dbContext.CourseInfo.Add(new CourseInfoDbM(editInfo));
            }
            else
            {
                course.CourseInfo.ApplyChanges(editInfo);
            }
            _dbContext.SaveChanges();
            return DbModel2ViewMode(course);
        }

        public IEnumerable<CoursePriceVM> LoadCoursePrices(int courseId)
        {
            CourseDbM course = _dbContext.Courses.Include("AdditionalPrices").
                FirstOrDefault(c => c.CourseId.Equals(courseId));

            return DbModel2ViewModel(course.AdditionalPrices);
        }

        public EditCourseInfoVM LoadCourseInfo(int courseId)
        {
            CourseInfoDbM info = _dbContext.CourseInfo.FirstOrDefault(c => c.CourseId == courseId);
            return DbModel2ViewModel(info);
        }


        public void CreatePrice(CoursePriceVM price)
        {
            CourseDbM course = _dbContext.Courses.FirstOrDefault(c => c.CourseId == price.CourseId);

            if (course == null)
                return;

            course.AddPrice(price);

            _dbContext.SaveChanges();
        }

        public void UpdatePrice(CoursePriceVM price)
        {
            CourseDbM course = _dbContext.Courses.FirstOrDefault(c => c.CourseId == price.CourseId);

            if (course == null)
                return;

            course.UpdatePrice(price);

            _dbContext.SaveChanges();
        }

        public void RemovePrice(CoursePriceVM price)
        {
            CourseDbM course = _dbContext.Courses.FirstOrDefault(c => c.CourseId == price.CourseId);

            if (course == null)
                return;

            CoursePriceDbM p = course.AdditionalPrices.FirstOrDefault(pr => pr.PriceId.Equals(price.PriceId));

            if (p != null)
            {
                _dbContext.CoursePrices.Remove(p);
                _dbContext.SaveChanges();
            }
        }

        public IPagedResult<FeedbackVM> LoadFeedbacks(IPageRequest pageRequest,int? courseFilter=null)
        {
            ulong totalItems = (uint)_dbContext.Feedbacks.Count();
            ushort pageCount = (ushort)(totalItems / pageRequest.PageSize + 1);
            ushort requestedPage = pageRequest.PageNumber;
            if (requestedPage < pageCount)
                requestedPage = pageCount;

            IEnumerable<FeedbackDbModel> feedbacks = null;

            if (pageRequest.All)
            {
                feedbacks = _dbContext.Feedbacks.OrderBy(f=>Guid.NewGuid()).Take(20).ToList();
            }
            else if (pageRequest.SortOrder == "desc")
            {
                feedbacks  = _dbContext.Feedbacks.OrderByDescending(f => f.Id).Skip(pageRequest.PageSize * (pageRequest.PageNumber - 1)).Take(pageRequest.PageSize).ToList();
            }
            else
            {
                feedbacks = _dbContext.Feedbacks.OrderBy(f => f.Id).Skip(pageRequest.PageSize * (pageRequest.PageNumber - 1)).Take(pageRequest.PageSize).ToList();
            }

            if (courseFilter.HasValue)
                feedbacks = feedbacks.Where(f => f.CourseId.Equals(courseFilter.Value));

            return new PagedResult<FeedbackVM>(DbModel2ViewModel(feedbacks), pageRequest.PageSize, pageRequest.PageNumber, pageCount, totalItems);
        }

        public void DeleteFeedback(int id)
        {
            FeedbackDbModel fModel = _dbContext.Feedbacks.FirstOrDefault(f => f.Id == id);
            if (fModel != null)
            {
                _dbContext.Feedbacks.Remove(fModel);
                _dbContext.SaveChanges();
            }
        }

        public FeedbackVM SaveFeedBack(FeedbackVM fb)
        {
            if (fb.Id > 0)
            {
                //update case
                FeedbackDbModel dbModel = _dbContext.Feedbacks.FirstOrDefault(f => f.Id == fb.Id);

                if (dbModel != null)
                {
                    dbModel.SetValues(fb.StudentName, fb.CourseId, fb.FeedBack, fb.ImageRef,
                        fb.VKProfileRef, fb.FBProfileRef, fb.OKProfileRef, DateTime.Now);

                    _dbContext.SaveChanges();


                    return DbModel2ViewModel(dbModel);
                }
            }
            else
            {
                //create case
                FeedbackDbModel newFeedback = new FeedbackDbModel();

                newFeedback.SetValues(fb.StudentName, fb.CourseId, fb.FeedBack, fb.ImageRef,
                    fb.VKProfileRef, fb.FBProfileRef, fb.OKProfileRef, DateTime.Now);

                _dbContext.Feedbacks.Add(newFeedback);

                _dbContext.SaveChanges();

                return DbModel2ViewModel(newFeedback);
            }

            return null;
        }

        public IEnumerable<StudentVM> LoadStudentsByFlow(int flowId)
        {
            IEnumerable<StudentDbM> students = _dbContext.Students.Where(s => s.FlowId == flowId).Include(s => s.ScheduledPayments);

            return DbStudentRepository.DbModel2ViewModel(students);
        }
    }
}
