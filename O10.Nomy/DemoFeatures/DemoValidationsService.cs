using Newtonsoft.Json;
using O10.Core.Architecture;
using O10.Nomy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.DemoFeatures
{
    [RegisterDefaultImplementation(typeof(IDemoValidationsService), Lifetime = LifetimeManagement.Scoped)]
    public class DemoValidationsService : IDemoValidationsService
    {
        private readonly Dictionary<string, FieldsAndPrompts> _fieldsAndPrompts = new()
        {
            { 
                "InvitationIntoGroup", 
                new()
                {
                    Static = new()
                    {
                        { "From", "" },
                        { "Date", "" }
                    },

                    Dynamic = new()
                    {
                        { "To", "" } 
                    }
                }
            }
        };

        private readonly IDataAccessService _dataAccessService;

        public DemoValidationsService(IDataAccessService dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        public Dictionary<string, string> GetDynamicFieldNamesAndPrompts(string action)
        {
            if(!_fieldsAndPrompts.TryGetValue(action, out var fieldsAndPrompts))
            {
                throw new ArgumentOutOfRangeException(nameof(action));
            }

            return fieldsAndPrompts.Dynamic;
        }

        public (Dictionary<string, bool> staticMap, Dictionary<string, string?> dynamicMap) GetMapsToFill(string action)
        {
            if (!_fieldsAndPrompts.TryGetValue(action, out var fieldsAndPrompts))
            {
                throw new ArgumentOutOfRangeException(nameof(action));
            }

            return (fieldsAndPrompts.Static.Select(s => s.Key).ToDictionary(s => s, s => false), fieldsAndPrompts.Dynamic.Select(s => s.Key).ToDictionary(s => s, s => null as string));
        }

        public async Task<List<ValidationEntryDTO>> GetValidation(long validationId, Dictionary<string, string> dynamicMap, CancellationToken cancellationToken)
        {
            var validationData = await _dataAccessService.GetDemoValidation(validationId, cancellationToken);
            var staticData = JsonConvert.DeserializeObject<Dictionary<string, bool>>(validationData.StaticData);
            var dynamicData = JsonConvert.DeserializeObject<Dictionary<string, string>>(validationData.DynamicData);

            return staticData.Select(s => new ValidationEntryDTO { Key = s.Key, Prompt = _fieldsAndPrompts[validationData.Action].Static[s.Key], Value = s.Value })
                             .Union(dynamicData.Select(s => new ValidationEntryDTO { Key = s.Key, Prompt = _fieldsAndPrompts[validationData.Action].Dynamic[s.Key], Value = s.Value == dynamicData.GetValueOrDefault(s.Key) }))
                             .ToList();
        }

        public async Task<long> StoreValidation(string action, Dictionary<string, bool> staticMap, Dictionary<string, string> dynamicMap, CancellationToken cancellationToken)
        {
            return await _dataAccessService.StoreDemoValidation(action, JsonConvert.SerializeObject(staticMap), JsonConvert.SerializeObject(dynamicMap), cancellationToken);
        }

        private record FieldsAndPrompts
        {
            internal Dictionary<string, string> Static { get; set; }
            internal Dictionary<string, string> Dynamic { get; set; }
        }
    }
}
