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

namespace HelloWorld.Extentions.Drivers
{
    public class StudentRegistrationDriver : ContentPartDriver<StudentRegistrationPart>
    {
		private readonly ICourseService _courseService;

		public StudentRegistrationDriver(ICourseService courseService)
		{
			_courseService = courseService;
		}

		protected override DriverResult Display(StudentRegistrationPart part, string displayType, dynamic shapeHelper)
        {
			CourseRegistrationVM vm = new CourseRegistrationVM
			{
				BlockData = part,
				Courses   = _courseService.GetCourses()
			};

            return ContentShape("Parts_StudentRegistration", () => shapeHelper.Parts_StudentRegistration(
                Model: vm
            ));
        }

        protected override DriverResult Editor(StudentRegistrationPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_StudentRegistration_Edit",
               () => shapeHelper.EditorTemplate(
                   TemplateName: "Parts/StudentRegistration",
                   Model: part,
                   Prefix: Prefix));
        }

        protected override DriverResult Editor(StudentRegistrationPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }

        protected override string Prefix
        {
            get
            {
                return "StudentRegistration";
            }
        }
    }
}