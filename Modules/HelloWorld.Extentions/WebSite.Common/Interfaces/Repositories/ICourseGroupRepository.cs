using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Models.ViewModels;

namespace WebSite.Common.Interfaces.Repositories
{
    public interface ICourseGroupRepository
    {
        IEnumerable<CourseGroupVM> LoadIntoUI();
        IPagedResult<CourseGroupVM> LoadPaged(IPageRequest pageRequest);
        CourseGroupVM CreateGroup(CreateGroupVM newGroup);
        CourseGroupVM UpdateGroup(EditGroupVM updatedGroup);
        CourseGroupVM UpdateHeaders(int groupId, EditPageHeadersVM newHeaders);
        void DeleteGroup(int groupId);
    }
}
