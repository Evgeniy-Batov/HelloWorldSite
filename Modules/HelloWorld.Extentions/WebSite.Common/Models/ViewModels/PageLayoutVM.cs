using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Common.Models.ViewModels
{
    public class PageLayoutVM
    {
        public PageLayoutVM()
        {
        }

        public PageLayoutVM(String name)
        {
            this.PageName = name;
        }

        public int Id { get; set; }
        [Required]
        public String PageName { get; set; }
        [Display(Name="Заголовок страницы")]
        [Required]
        public String PageTitle { get; set; }
        [Display(Name = "Заголовок H1")]
        [Required]
        public String TitleH1 { get; set; }
        [Display(Name = "Meta Description")]
        [Required]
        public String Description { get; set; }
        [Display(Name = "Meta Keywords")]
        public String KeyWords { get; set; }
        [Display(Name = "Meta Robots")]
        public String Robots { get; set; }
        [Display(Name = "HTML")]
        public String Markup { get; set; }
    }
}
