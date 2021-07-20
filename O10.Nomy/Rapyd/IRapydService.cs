using O10.Core.Architecture;
using O10.Nomy.DTOs;
using System.Threading.Tasks;

namespace O10.Nomy.Rapyd
{
    [ServiceContract]
    public interface IRapydService
    {
        Task<string> CreateRapydWallet(UserDTO user);
        Task<string> CreateBenificiary(UserDTO user);
        Task<string> CreateSender(UserDTO user);

        Task<ulong?> ReplenishFunds(string walletId, int threshold, ulong target);

        Task TransferFunds(string sourceWalletId, string destinationWalletId, string currency, ulong amount);

        Task PayoutFunds(string sourceWalletId, string beneficiaryId, string currency, ulong amount);
    }
}
