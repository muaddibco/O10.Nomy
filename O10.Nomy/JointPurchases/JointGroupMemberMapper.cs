using O10.Core.Architecture;
using O10.Core.Translators;
using O10.Nomy.JointPurchases.Models;
using O10.Nomy.Models;

namespace O10.Nomy.JointPurchases
{
    [RegisterExtension(typeof(ITranslator), Lifetime = LifetimeManagement.Transient)]
    public class JointGroupMemberMapper : TranslatorBase<JointGroupMember, JointGroupMemberDTO>
    {
        public override JointGroupMemberDTO Translate(JointGroupMember obj)
        {
            return new JointGroupMemberDTO
            {
                JointGroupMemberId = obj.JointGroupMemberId,
                Email = obj.Email,
                Description = obj.Description
            };
        }
    }
}
