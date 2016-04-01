using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.LearnOrchard.FeaturedProduct.Models
{
    public class FeaturedProductPartRecord: ContentPartRecord
    {
        public virtual bool IsOnSale { get; set; }
    }
}