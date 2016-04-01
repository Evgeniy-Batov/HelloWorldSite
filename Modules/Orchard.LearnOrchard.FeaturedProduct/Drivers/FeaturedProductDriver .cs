using Orchard.ContentManagement.Drivers;
using Orchard.LearnOrchard.FeaturedProduct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.CompilerServices;
using Orchard.ContentManagement;
using Orchard.Alias;

namespace Orchard.LearnOrchard.FeaturedProduct.Drivers
{
    public class FeaturedProductDriver: ContentPartDriver<FeaturedProductPart>
    {
        private IContent _currentContent = null;

        private readonly IContentManager _contentManager;
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IAliasService _aliasService;

        public FeaturedProductDriver(IContentManager contentManager,IWorkContextAccessor workContextAccessor,IAliasService aliasService)
        {
            _contentManager = contentManager;
            _workContextAccessor = workContextAccessor;
            _aliasService = aliasService;
        }


        private IContent CurrentContent
        {
            get
            {
                if (_currentContent == null)
                {
                    var itemRoute = _aliasService.Get(_workContextAccessor.GetContext()
                      .HttpContext.Request.AppRelativeCurrentExecutionFilePath
                      .Substring(1).Trim('/'));

                    _currentContent = _contentManager.Get(Convert.ToInt32(itemRoute["Id"]));
                }

                return _currentContent;
            }
        }


        protected override DriverResult Display(FeaturedProductPart part,
            string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_FeaturedProduct", () => {
                bool isOnFeaturedProductPage = false;

                if (this.CurrentContent != null)
                {
                    String itemTypeName = CurrentContent.ContentItem.TypeDefinition.Name;

                    if (itemTypeName.Equals("Product",StringComparison.InvariantCultureIgnoreCase))
                    {
                        // final product id check will go here
                        var dynamicContentItem = (dynamic)CurrentContent.ContentItem;
                        var itemProductId = dynamicContentItem.Product.ProductId.Value;
                        if (itemProductId.Equals("SPROCKET9000",
                          StringComparison.InvariantCulture))
                        {
                            isOnFeaturedProductPage = true;
                        }
                    }
                }

                // extra space to write additional lines of code here
                return shapeHelper.Parts_FeaturedProduct(IsOnFeaturedProductPage: isOnFeaturedProductPage);
            });
        }

        protected override DriverResult Editor(FeaturedProductPart part,dynamic shapeHelper)
        {
            return ContentShape("Parts_FeaturedProduct_Edit", () => shapeHelper.EditorTemplate(
                 TemplateName: "Parts/FeaturedProduct",
                 Model: part,
                 Prefix: Prefix
                ));
        }

        protected override DriverResult Editor(FeaturedProductPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }
    }
}