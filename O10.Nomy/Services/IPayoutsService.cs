using O10.Core.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    [ServiceContract]
    public interface IPayoutsService
    {
        Task CollectPayments(CancellationToken ct);
    }
}
