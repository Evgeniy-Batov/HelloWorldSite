using HelloWorld.Extentions.Models;
using Orchard.ContentManagement.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.CompilerServices;
using Orchard.ContentManagement;

namespace HelloWorld.Extentions.Drivers
{
    public class CoursePartDriver:ContentPartDriver<CoursePart>
    {
        protected override string Prefix
        {
            get
            {
                return "Course";
            }
        }

        protected override DriverResult Editor(CoursePart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Course_Edit", () => shapeHelper
                .EditorTemplate(TemplateName: "Parts/Course", Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(CoursePart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }

        protected override DriverResult Display(CoursePart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Course", () => shapeHelper.Parts_Course(
                    Name: part.Name,
                    Title: part.Title,
                    ShortDescription: part.ShortDescription
                ));
        }
    }
}