using O10.Client.Common;
using System.Collections.Generic;
using System.Linq;

namespace O10.Nomy
{
    public class WebApiBootstrapper : ClientBootstrapper
    {
        private readonly string[] _catalogItems = new string[] { "O10.Nomy.dll" };

        protected override IEnumerable<string> EnumerateCatalogItems(string rootFolder)
        {
            return base.EnumerateCatalogItems(rootFolder)
                .Concat(_catalogItems);
                //.Concat(Directory.EnumerateFiles(rootFolder, "O10.IdentityProvider.DataLayer*.dll").Select(f => new FileInfo(f).Name))
                //.Concat(Directory.EnumerateFiles(rootFolder, "O10.Integrations.*.dll").Select(f => new FileInfo(f).Name));
        }
    }
}
