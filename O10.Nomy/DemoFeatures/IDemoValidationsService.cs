using O10.Core.Architecture;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.DemoFeatures
{
    [ServiceContract]
    internal interface IDemoValidationsService
    {
        (Dictionary<string, bool> staticMap, Dictionary<string, string?> dynamicMap) GetMapsToFill(string action);
        Dictionary<string, string> GetDynamicFieldNamesAndPrompts(string action);
        Task<long> StoreValidation(string action, Dictionary<string, bool> staticMap, Dictionary<string, string> dynamicMap, CancellationToken cancellationToken);

        Task<List<ValidationEntryDTO>> GetValidation(long validationId, Dictionary<string, string> dynamicMap, CancellationToken cancellationToken);
    }
}
