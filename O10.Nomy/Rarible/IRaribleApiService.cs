using O10.Core.Architecture;
using O10.Nomy.Rarible.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace O10.Nomy.Rarible
{
    [ServiceContract]
    public interface IRaribleApiService
    {
        Task<AllItemsDto> GetAllItems(string continuationToken = null);
    }
}
