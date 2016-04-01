using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using WebSite.Common.Models.ViewModels;

namespace WebSite.Models
{
    public class TrainingPageVM
    {
        private ICollection<CourseCategoryVM> _courceCategories = new List<CourseCategoryVM>();

        public String FilterLabel { get; set; }

        public PageLayoutVM Layout { get; set; }

        public String GetBreadCrumbLabel()
        {
            CourseCategoryVM cat = this.GetSelected();
            return cat == null ? String.Empty : cat.Group.BreadCrumb;
        }

        public bool ContainsCategory(String categoryName)
        {
            foreach (CourseCategoryVM cc in this.Categories)
            {
                if (String.Compare(cc.ParamName, categoryName, true) == 0)
                    return true;
            }
            return false;
        }

        public TrainingPageVM()
        {
            _courceCategories = new List<CourseCategoryVM>();
        }

        public IEnumerable<CourseCategoryVM> Categories
        {
            get
            {
                return _courceCategories;
            }
        }

        public CourseCategoryVM GetSelected()
        {
            return _courceCategories.Where(c => c.Selected).FirstOrDefault();
        }

        public void SetSelection(String selectedCategory)
        {
            if (String.IsNullOrEmpty(selectedCategory))
                return;

            foreach (CourseCategoryVM cat in _courceCategories)
            {
                if (String.Compare(cat.ParamName, selectedCategory, true, CultureInfo.InvariantCulture) == 0)
                {
                    cat.Selected = true;
                }
            }
        }

        public void AddCourceCategory(CourseCategoryVM category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            _courceCategories.Add(category);
        }


        public class CourseCategoryVM
        {
            public String  Name      {get;set;}
            public String  ParamName {get;set;}
            public Boolean Selected  {get;set;}

            public CourseGroupVM  Group { get; set; }
            public CourseVM      Course { get; set; }

            public String CustomCSS { get; set; }

            public IEnumerable<CourseCategoryVM> ChildCategories {get;set;}
        }
    }
}