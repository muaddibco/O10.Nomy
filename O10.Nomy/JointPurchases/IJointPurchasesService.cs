using O10.Core.Architecture;
using O10.Nomy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.JointPurchases
{
    [ServiceContract]
    public interface IJointPurchasesService
    {
        Task Initialize(CancellationToken ct);

        Task<NomyServiceProvider> GetJointServiceRecord();
    }
}
