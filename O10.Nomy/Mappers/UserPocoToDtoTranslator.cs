using O10.Core.Architecture;
using O10.Core.Translators;
using O10.Nomy.DTOs;
using O10.Nomy.Models;

namespace O10.Nomy.Mappers
{
    [RegisterExtension(typeof(ITranslator), Lifetime = LifetimeManagement.Singleton)]
    public class UserPocoToDtoTranslator : TranslatorBase<NomyUser?, UserDetailsDTO?>
    {
        public override UserDetailsDTO? Translate(NomyUser? obj)
        {
            if(obj == null)
            {
                return null;
            }

            return new UserDetailsDTO
            {
                AccountId = obj.NomyUserId,
                Email = obj.Email,
                O10Id = obj.O10Id,
                WalletId = obj.WalletId
            };
        }
    }
}
