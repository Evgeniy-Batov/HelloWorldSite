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
	public class SessionPartDriver: ContentPartDriver<SessionDataPart>
	{
		protected override DriverResult Display(SessionDataPart part, string displayType,dynamic shapeHelper)
		{
			return ContentShape("Parts_SessionData",
			   () => shapeHelper.Parts_Feedback(
					   ContentPart: part,
					   ProperyName: part.ProperyName,
					   TempData: part.TempData
				   ));
		}

		protected override DriverResult Editor(SessionDataPart part,dynamic shapeHelper)
		{
			return ContentShape("Parts_SessionData_Edit",
			() => shapeHelper.EditorTemplate(
					 ProperyName: part.ProperyName,
					 TempData: part.TempData,
                     Prefix: Prefix
				));
		}

		protected override DriverResult Editor(SessionDataPart part, IUpdateModel updater, dynamic shapeHelper)
		{

			updater.TryUpdateModel(part, Prefix, null, null);

			return Editor(part, shapeHelper);
		}

		protected override string Prefix
		{
			get
			{
				return "SessionData";
			}
		}
	}
}