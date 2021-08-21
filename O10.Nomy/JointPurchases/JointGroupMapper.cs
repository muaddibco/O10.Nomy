using O10.Core.Architecture;
using O10.Core.Translators;
using O10.Nomy.JointPurchases.Models;
using O10.Nomy.Models;

namespace O10.Nomy.JointPurchases
{
    [RegisterExtension(typeof(ITranslator), Lifetime = LifetimeManagement.Transient)]
    public class JointGroupMapper : TranslatorBase<JointGroup, JointGroupDTO>
    {
        public override JointGroupDTO Translate(JointGroup obj)
        {
            return new JointGroupDTO
            {
                JointGroupId = obj.JointGroupId,
                O10RegistrationId = obj.O10RegistrationId,
                Name = obj.Name,
                Description = obj.Description
            };
        }
    }
}
