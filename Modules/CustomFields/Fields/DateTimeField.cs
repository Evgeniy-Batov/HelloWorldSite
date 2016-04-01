using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CustomFields.DateTimeField.Fields
{
    [OrchardFeature("DateTimeField")]
    public class DateTimeField : ContentField
    {

        public DateTime? DateTime
        {
            get
            {
                var value = Storage.Get<string>("DateTimeField");
                DateTime parsedDateTime;

                if (System.DateTime.TryParse(value, CultureInfo.InvariantCulture,
                    DateTimeStyles.AdjustToUniversal, out parsedDateTime))
                {

                    return parsedDateTime;
                }

                return null;
            }

            set
            {
                Storage.Set("DateTimeField",value == null ?
                    String.Empty :
                    value.Value.ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}