using O10.Core.Architecture;
using O10.Nomy.Rapyd.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace O10.Nomy.Rapyd
{
    [ServiceContract]
    public interface IRapydApi
    {
        Task<WalletResponseDTO?> CreateWallet(WalletRequestDTO wallet);

        Task<WalletResponseDTO?> GetWallet(string id);

        Task<DepositFundsResponseDTO?> DepositFunds(string walletId, string currency, ulong amount);

        Task<PutFundsOnHoldResponseDTO?> PutFundsOnHold(string walletId, string currency, ulong amount);
    }
}
