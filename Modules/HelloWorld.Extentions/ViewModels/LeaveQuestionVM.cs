using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWorld.Extentions.ViewModels
{
	public class LeaveQuestionVM
	{
		[Required]
		public String Name { get; set; }

		[Required]
		public String Email { get; set; }

		public String Phone { get; set; }

		public String Subject { get; set; }

		[Required]
		public String Question { get; set; }
	}
}