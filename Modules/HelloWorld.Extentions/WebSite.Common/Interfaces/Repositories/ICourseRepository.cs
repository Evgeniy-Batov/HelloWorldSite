using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Models.ViewModels;

namespace WebSite.Common.Interfaces.Repositories
{
    public interface ICourseRepository
    {
        EditCourseInfoVM LoadCourseInfo(int courseId);
        CourseVM LoadById(int id);
        CourseVM LoadByPath(String path);
        IPagedResult<CourseVM> LoadPaged(IPageRequest pageRequest);
        CourseVM UpdateCourseInfo(EditCourseInfoVM editInfo);
        CourseVM UpdateCourse(EditCourseVM editVM);
        CourseVM CreateCourse(CreateCourseVM createVM);
        void DeleteCourse(int courseId);
        CourseVM UpdateHeaders(int courseId, EditPageHeadersVM newHeaders);

        ICollection<CourseFlowVM> LoadCourseFlows(int courseId,FlowStatus? status);
        IEnumerable<CoursePriceVM> LoadCoursePrices(int courseId);

        void CreatePrice(CoursePriceVM price);
        void UpdatePrice(CoursePriceVM price);
        void RemovePrice(CoursePriceVM price);

        CourseFlowVM LoadFlow(int flowId);
        CourseFlowVM CreateFlow(CourseFlowVM newFlow);
        CourseFlowVM UpdateFlow(CourseFlowVM updatedFlow);
        IPagedResult<FeedbackVM> LoadFeedbacks(IPageRequest pageRequest, int? courseFilter = null);
        FeedbackVM SaveFeedBack(FeedbackVM feedBack);
        void DeleteFeedback(int id);

        IEnumerable<StudentVM> LoadStudentsByFlow(int flowId);
    }
}
