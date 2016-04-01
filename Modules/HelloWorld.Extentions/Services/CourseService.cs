using HelloWorld.Extentions.Models;
using HelloWorld.Extentions.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.Extentions.Services
{
    public interface ICourseService : IDependency
    {
        CoursePartRecord GetCourse(int courseId);

        IEnumerable<CoursePartRecord> GetCourses();

        void UpdateCourseForContentItem(ContentItem item, EditFeedbackViewModel model);

    }

    public class CourseService : ICourseService
    {
        private readonly IRepository<CoursePartRecord> _courseRepository;
        private readonly IContentManager               _contentManager;

        public CourseService(IRepository<CoursePartRecord> courseRepository, IContentManager contentManager)
        {
            _courseRepository = courseRepository;
            _contentManager   = contentManager;
        }

        public CoursePartRecord GetCourse(int courseId)
        {
            return _courseRepository.Table.FirstOrDefault(c => c.Id.Equals(courseId));
        }

        public IEnumerable<CoursePartRecord> GetCourses()
        {
            var courses = _contentManager.List<ContentPart>(new string[] { "CoursePage" });

            List<CoursePartRecord> dbResult = _courseRepository.Table.ToList();
            List<CoursePartRecord> filtered = new List<CoursePartRecord>();

            foreach (CoursePartRecord r in dbResult)
            {
                if (courses.Where(c=>c.Id == r.Id).FirstOrDefault() != null)
                    filtered.Add(r);
            }

            return filtered;        
        }

        public void UpdateCourseForContentItem(ContentItem item, EditFeedbackViewModel model)
        {
            FeedbackPart feedbackPart = item.As<FeedbackPart>();

            feedbackPart.AuthorName   = model.Author;
            feedbackPart.CourseId     = model.CourseId; //_courseRepository.Get(c => c.Id == model.CourseId);
        }
    }
}