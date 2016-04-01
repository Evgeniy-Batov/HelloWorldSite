using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Helpers
{
    public static class Html
    {
        public static MvcHtmlString CheckBoxForJson(this HtmlHelper helper,String propName,String id)
        {
            string html = "<input type=\"checkbox\" name=\""
                + propName + "\" id=\""
                + id + "\" value=\"true\" autocomplete=\"off\" />";
            return MvcHtmlString.Create(html);
        }
    }
}
