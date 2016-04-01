using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Interfaces.Repositories;
using WebSite.Common.Models.ViewModels;
using WebSite.DAL.Db.Models;
using WebSite.DAL.Vistadb.Repositories;

namespace WebSite.DAL.Db.Repositories
{
    public class DbCourseGroupRepository: ICourseGroupRepository
    {
        private Context _dbContext;

        public DbCourseGroupRepository(String connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
                throw new ArithmeticException("connectionString");

            _dbContext = new Context(connectionString);
        }

        private CourseGroupDbM ViewModel2DbModel(CreateGroupVM vm)
        {
            CourseGroupDbM result = new CourseGroupDbM();
            result.BreadCrumb = vm.BreadCrumb;
            result.GoalsMarkup = vm.GoalsMarkup;
            result.GroupName = vm.GroupName;
            result.ImagePath = vm.ImagePath;
            result.MethodMarkup = vm.MethodMarkup;
            result.Order = vm.Order;
            result.RouteName = vm.RouteName;
            result.SolutionsMarkup = vm.SolutionsMarkup;

            result.TitleH1 = String.Empty;
            result.Description = String.Empty;
            result.KeyWords = String.Empty;
            result.Robots = String.Empty;
            result.PageTitle = String.Empty;

            result.CustomPageHtml = vm.CustomPageHtml;
            result.MenuItemStyle  = vm.MenuItemStyle;
            result.IsNew          = vm.IsNew == "on";
           
            return result;
        }

        public static CourseGroupVM DbModel2ViewMode(CourseGroupDbM model)
        {
            CourseGroupVM result = new CourseGroupVM();
            result.BreadCrumb = model.BreadCrumb;
            result.GoalsMarkup = model.GoalsMarkup;
            result.GroupId = model.GroupId;
            result.GroupName = model.GroupName;
            result.ImagePath = model.ImagePath;
            result.MethodMarkup = model.MethodMarkup;
            result.Order = model.Order;
            result.RouteName = model.RouteName;
            result.SolutionsMarkup = model.SolutionsMarkup;

            result.TitleH1      = model.TitleH1;
            result.Description  = model.Description;
            result.KeyWords     = model.KeyWords;
            result.Robots       = model.Robots;
            result.PageTitle    = model.PageTitle;

            result.CustomPageHtml = model.CustomPageHtml;
            result.MenuItemStyle = model.MenuItemStyle;
            result.IsNew = model.IsNew.HasValue ? model.IsNew.Value : false;


            if (model.Courses != null)
                result.Courses = DbCourseRepository.DbModel2ViewMode(model.Courses);

            return result;
        }

        private IEnumerable<CourseGroupVM> DbModel2ViewMode(IEnumerable<CourseGroupDbM> groups)
        {
            List<CourseGroupVM> result = new List<CourseGroupVM>();
            if (groups != null)
            {
                foreach (CourseGroupDbM g in groups)
                {
                    result.Add(DbModel2ViewMode(g));
                }
            }
            return result;
        }

        public CourseGroupVM UpdateHeaders(int groupId, EditPageHeadersVM newHeaders)
        {
            CourseGroupDbM existingGroup = _dbContext.CourseGroups.FirstOrDefault(g => g.GroupId == groupId);
            existingGroup.ModifyHeaders(newHeaders);
            _dbContext.SaveChanges();
            return DbModel2ViewMode(existingGroup);
        }

        public IEnumerable<CourseGroupVM> LoadIntoUI()
        {
            List<CourseGroupVM> result = new List<CourseGroupVM>();

            List<CourseGroupDbM> dbRes = _dbContext.CourseGroups.Include("Courses.AdditionalPrices").Where(c => c.Order >= 0).OrderBy(c => c.Order).ToList();

            return DbModel2ViewMode(dbRes);
        }

        public CourseGroupVM CreateGroup(CreateGroupVM newGroup)
        {
            CourseGroupDbM newDbModel = ViewModel2DbModel(newGroup);
            _dbContext.CourseGroups.Add(newDbModel);
            _dbContext.SaveChanges();

            return DbModel2ViewMode(newDbModel);

        }

        public CourseGroupVM UpdateGroup(EditGroupVM updatedGroup)
        {
            CourseGroupDbM existingGroup = _dbContext.CourseGroups.FirstOrDefault(g => g.GroupId == updatedGroup.GroupId);
            if (existingGroup != null)
            {
                existingGroup.Modify(updatedGroup);
                _dbContext.SaveChanges();
                return DbModel2ViewMode(existingGroup);
            }
            return null;
        }

        public void DeleteGroup(int groupId)
        {
            CourseGroupDbM existingGRoup = _dbContext.CourseGroups.FirstOrDefault(g => g.GroupId == groupId);
            if (existingGRoup != null)
            {
                _dbContext.CourseGroups.Remove(existingGRoup);
                _dbContext.SaveChanges();
            }
        }

        public IPagedResult<CourseGroupVM> LoadPaged(IPageRequest pageRequest)
        {
            ulong  totalMessages = (uint)_dbContext.CourseGroups.Count();
            ushort pageCount = (ushort)(totalMessages / pageRequest.PageSize + 1);

            ushort requestedPage = pageRequest.PageNumber;
            if (requestedPage < pageCount)
                requestedPage = pageCount;

            IEnumerable<CourseGroupDbM> groups = null;
            if (pageRequest.All)
            {
                groups = _dbContext.CourseGroups.ToList();
            }
            else if (pageRequest.SortOrder == "desc")
            {
                groups = _dbContext.CourseGroups.OrderByDescending(m => m.GroupId).Skip(pageRequest.PageSize * (pageRequest.PageNumber - 1)).Take(pageRequest.PageSize).ToList();
            }
            else
            {
                groups = _dbContext.CourseGroups.OrderBy(m => m.GroupId).Skip(pageRequest.PageSize * (pageRequest.PageNumber - 1)).Take(pageRequest.PageSize).ToList();
            }

            return new PagedResult<CourseGroupVM>(DbModel2ViewMode(groups), pageRequest.PageSize, pageRequest.PageNumber, pageCount, totalMessages);
        }
    }
}
