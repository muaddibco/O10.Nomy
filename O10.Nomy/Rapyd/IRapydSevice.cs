using O10.Core.Architecture;
using O10.Nomy.DTOs;
using System.Threading.Tasks;

namespace O10.Nomy.Rapyd
{
    [ServiceContract]
    public interface IRapydSevice
    {
        Task<string> CreateRapydWallet(UserDTO user);
        Task<string> CreateBeneficiary(UserDTO user);
        Task<string> CreateSender(UserDTO user);

        Task<ulong?> ReplenishFunds(string walletId, int threshold, ulong target);
    }
}
