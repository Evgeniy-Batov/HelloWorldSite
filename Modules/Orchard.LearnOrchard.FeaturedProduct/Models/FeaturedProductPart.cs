using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Orchard.LearnOrchard.FeaturedProduct.Models
{
    public class FeaturedProductPart: ContentPart<FeaturedProductPartRecord>
    {
        [DisplayName("Is the featured product on sale?")]
        public bool IsOnSale
        {
            get { return Retrieve(r => r.IsOnSale); }
            set { Store(r => r.IsOnSale, value); }
        }
    }
}