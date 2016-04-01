using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Common.Models.ViewModels;

namespace HelloWorld.Extentions.ViewModels 
{
	public class FindScheduledPaymentsVM
	{
		public IEnumerable<ScheduledPaymentVM> Payments { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
	}
}