using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using SimpleCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleCommerce.Handlers
{
    public class ProductHandler : ContentHandler
    {
        public ProductHandler(IRepository<ProductPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}