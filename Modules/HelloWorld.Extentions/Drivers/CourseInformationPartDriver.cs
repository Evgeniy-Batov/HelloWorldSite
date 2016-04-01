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
    public class CourseInformationPartDriver:ContentPartDriver<CourseInformationPart>
    {
        protected override string Prefix
        {
            get
            {
                return "CourseInformation";
            }
        }

        protected override DriverResult Display(CourseInformationPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_CourseInformation", () => shapeHelper.Parts_CourseInformation(
                  GetPartInfo: part 
            ));
        }

        protected override DriverResult Editor(CourseInformationPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_CourseInformation_Edit", () => shapeHelper
               .EditorTemplate(TemplateName: "Parts/CourseInformation", Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(CourseInformationPart part, IUpdateModel updater,  dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }
    }
}