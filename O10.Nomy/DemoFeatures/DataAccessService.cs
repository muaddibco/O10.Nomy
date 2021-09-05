using Microsoft.EntityFrameworkCore;
using O10.Nomy.DemoFeatures.Models;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    public partial class DataAccessService
    {
        public async Task<long> StoreDemoValidation(string action, string staticData, string dynamicData, CancellationToken cancellationToken)
        {
            var entry = _dbContext.DemoValidations.Add(new DemoValidation
            {
                Action = action,
                StaticData = staticData,
                DynamicData = dynamicData
            });

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entry.Entity.DemoValidationId;
        }

        public async Task<DemoValidation> GetDemoValidation(long demoValiationId, CancellationToken cancellationToken)
        {
            return await _dbContext.DemoValidations.FirstOrDefaultAsync(s => s.DemoValidationId == demoValiationId, cancellationToken);
        }
    }
}
