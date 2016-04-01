using System.Linq;
using Orchard;
using Orchard.DisplayManagement.Descriptors;
using Orchard.Environment.Extensions;
using Orchard.UI.Resources;
using System.IO;
using System.Web;

namespace Bootstrap_3_2_0_Base.Providers
{
    public class ShapeTable : IShapeTableProvider
    {
        private readonly IWorkContextAccessor _wca;

        private string faviconThemeRelativeUrl = "/Content/favicon.ico";

        public ShapeTable(IWorkContextAccessor wca)
        {
            _wca = wca;
        }

        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("HeadLinks")
                .OnDisplaying(shapeDisplayingContext =>
                {
                    // e.g. "/Themes/MyThemeName//Content/favicon.ico"
                    var currentTheme = _wca.GetContext().CurrentTheme;
                    var faviconRelativeUrl = (currentTheme.Location + "/" + currentTheme.Path + "/" + faviconThemeRelativeUrl).TrimStart('~');

                    // Get the current favicon from head
                    var resourceManager = _wca.GetContext().Resolve<IResourceManager>();
                    var links = resourceManager.GetRegisteredLinks();
                    var currentFavicon = links
                        .Where(l => l.Rel == "shortcut icon" && l.Type == "image/x-icon")
                        .FirstOrDefault();

                    // Modify if found
                    if (currentFavicon != default(LinkEntry))
                    {
                        currentFavicon.Href = faviconRelativeUrl;
                    }
                    else
                    {
                        // Add the new one
                        resourceManager.RegisterLink(new LinkEntry
                        {
                            Type = "image/x-icon",
                            Rel = "shortcut icon",
                            Href = faviconRelativeUrl
                        });
                    }
                });
        }
    }
}