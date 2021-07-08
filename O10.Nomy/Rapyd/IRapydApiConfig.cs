using O10.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace O10.Nomy.Rapyd
{
    public interface IRapydApiConfig : IConfigurationSection
    {
        string AccessKey { get; set; }
        string SecretKey { get; set; }
        string BaseUri { get; set; }
    }
}
