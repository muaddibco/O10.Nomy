using O10.Core.Architecture;
using O10.Nomy.Rapyd.DTOs;
using O10.Nomy.Rapyd.DTOs.Disburse;
using System.Threading.Tasks;

namespace O10.Nomy.Rapyd
{
    [ServiceContract]
    public interface IRapydApi
    {
        Task<WalletResponseDTO?> CreateWallet(WalletRequestDTO wallet);

        Task<WalletResponseDTO?> GetWallet(string id);

        Task<DepositFundsResponseDTO?> DepositFunds(string walletId, string currency, ulong amount);

        Task<PutOnHoldReleaseFundsResponseDTO?> PutFundsOnHold(string walletId, string currency, ulong amount);
        Task<PutOnHoldReleaseFundsResponseDTO?> ReleaseFunds(string walletId, string currency, ulong amount);

        Task<BeneficiaryDTO?> CreateBenificiary(BeneficiaryDTO beneficiary);

        Task<SenderDTO?> CreateSender(SenderDTO sender);

        Task<TransferFundsResponseDTO?> TransferFunds(string sourceWalletId, string destinationWalletId, string currency, ulong amount);

        Task<TransferFundsResponseDTO?> ConfirmTransfer(string transferId);

        Task<string> PayoutFunds(string sourceWalletId, string beneficiaryId, string currency, ulong amount);
    }
}
