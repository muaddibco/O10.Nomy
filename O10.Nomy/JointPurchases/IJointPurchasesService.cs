using O10.Core.Architecture;
using O10.Nomy.JointPurchases.Models;
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
        Task<NomyServiceProvider> GetJointServiceRecord(CancellationToken ct);

        Task<JointGroupDTO> AddJointGroup(long o10RegistrationId, string name, string description, CancellationToken ct);

        Task<List<JointGroupDTO>> GetJointGroups(long o10RegistrationId, CancellationToken ct);

        Task<JointGroupMemberDTO> AddJointGroupMember(long groupId, string email, string? description, CancellationToken ct);

        Task<List<JointGroupMemberDTO>> GetJointGroupMembers(long groupId, CancellationToken ct);
    }
}
