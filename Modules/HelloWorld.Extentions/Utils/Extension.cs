using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using WebSite.Common.Utils;

namespace WebSite.Helpers
{
    public static class Extension
    {
        public static MvcHtmlString EnumDropDownListFor<TModel, TProperty, TEnum>(
                    this HtmlHelper<TModel> htmlHelper,
                    Expression<Func<TModel, TProperty>> expression,
                    TEnum selectedValue)
        {
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum))
                                        .Cast<TEnum>();
            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem()
                                                {
                                                    Text = EnumHelper<TEnum>.GetDisplayValue(value),
                                                    Value = value.ToString(),
                                                    Selected = (value.Equals(selectedValue))
                                                };

            return SelectExtensions.DropDownListFor(htmlHelper, expression, items);
        }
    }
}