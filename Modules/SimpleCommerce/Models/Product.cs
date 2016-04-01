using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleCommerce.Models
{
    public class ProductPartRecord : ContentPartRecord
    {
        public virtual string Sku { get; set; }
        public virtual float Price { get; set; }
    }

    public class ProductPart : ContentPart<ProductPartRecord>
    {
        [Required]
        public string Sku
        {
            get { return Retrieve(r => r.Sku); }
            set { Store(r => r.Sku, value); }
        }

        [Required]
        public float Price
        {
            get { return Retrieve(r => r.Price); }
            set { Store(r => r.Price, value); }
        }
    }
}