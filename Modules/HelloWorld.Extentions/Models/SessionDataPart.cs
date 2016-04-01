using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.Extentions.Models
{
	public class SessionDataPartRecord : ContentPartRecord
	{
		public virtual String PropertyName { get; set; }
		public virtual bool	  TempData	   { get; set; }
	}

	public class SessionDataPart: ContentPart<SessionDataPartRecord>
	{
		public String ProperyName
		{
			get { return Retrieve(r => r.PropertyName); }
			set { Store(r => r.PropertyName,value); }
		}

		public bool TempData
		{
			get { return Retrieve(r => r.TempData); }
			set { Store(r => r.TempData, value); }
		}
	}
}