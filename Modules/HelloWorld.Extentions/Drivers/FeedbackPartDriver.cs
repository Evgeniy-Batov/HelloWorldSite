using HelloWorld.Extentions.Models;
using Orchard.ContentManagement.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.CompilerServices;
using Orchard.ContentManagement;
using HelloWorld.Extentions.ViewModels;
using HelloWorld.Extentions.Services;
using JetBrains.Annotations;

namespace HelloWorld.Extentions.Drivers
{
    [UsedImplicitly]
    public class FeedbackPartDriver: ContentPartDriver<FeedbackPart>
    {
        private readonly ICourseService _courseService;

        private const string TemplateName = "Parts/Feedback";

        public FeedbackPartDriver(ICourseService courseService)
        {
            _courseService = courseService;
        }

        protected override DriverResult Display(FeedbackPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Feedback",
                () => shapeHelper.Parts_Feedback(
                        ContentPart :part,
                        Course      : part.CourseId < 1 ? null : _courseService.GetCourse(part.CourseId),
                        CourseId    : part.CourseId,
                        Author      :part.AuthorName
                    ));
        }

        private EditFeedbackViewModel BuildEditorViewModel(FeedbackPart part)
        {
            EditFeedbackViewModel vm = new EditFeedbackViewModel()
            {
                Author = part.AuthorName,
                Courses = _courseService.GetCourses(),
                CourseId = part.CourseId
            };

            //if (part.Course != null)
            //{
            //    vm.CourseId = part.Course.Id;
            //}

            return vm;
        }

        protected override DriverResult Editor(FeedbackPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Feedback_Edit",
                () => shapeHelper.EditorTemplate(
                        TemplateName: TemplateName,
                        Model: BuildEditorViewModel(part),
                        Prefix:Prefix
                    ));
        }

        protected override DriverResult Editor(FeedbackPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            EditFeedbackViewModel model = new EditFeedbackViewModel();

            if (updater.TryUpdateModel(model, Prefix,null,null))
            {
                if (part.ContentItem.Id != 0)
                {
                    _courseService.UpdateCourseForContentItem(part.ContentItem, model);
                }
            }


            FeedbackPart feedbackPart = part.As<FeedbackPart>();

            model.Author  = feedbackPart.AuthorName;
            model.CourseId = feedbackPart.CourseId;

            return Editor(feedbackPart, shapeHelper);
        }


        protected override string Prefix
        {
            get
            {
                return "Feedback";
            }
        }
    }
}