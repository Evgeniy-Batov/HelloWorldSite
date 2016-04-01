using Orchard.UI.Resources;

namespace Bootstrap_3_1_1_Base.Providers
{
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineScript("Bootstrap").SetUrl("bootstrap.min.js").SetVersion("3.2.0").SetDependencies("jQuery");
            manifest.DefineStyle("Bootstrap").SetUrl("Bootstrap/less/bootstrap.min.css").SetVersion("3.2.0");
        }
    }
}
