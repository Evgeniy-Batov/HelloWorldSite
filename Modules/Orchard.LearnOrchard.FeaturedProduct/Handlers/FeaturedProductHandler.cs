using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.LearnOrchard.FeaturedProduct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.LearnOrchard.FeaturedProduct.Handlers
{
    public class FeaturedProductHandler:ContentHandler
    {
        public FeaturedProductHandler(IRepository<FeaturedProductPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }

    }
}