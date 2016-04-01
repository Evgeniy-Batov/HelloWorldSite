using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWorld.Extentions.ViewModels
{
	public class RegistrationVM
	{
		[Required]
		public String FirstName  { get; set; }

		[Required]
		public String MiddleName { get; set; }

		[Required]
		public String LastName   { get; set; }

		[Required]
		public String Email      { get; set; }

		[Required]
		public String Phone     { get; set; }

		[Required]
		public String CourseId   { get; set; }

		public String Comment	 { get; set; }
	}
}