using O10.Core;
using O10.Core.Architecture;
using O10.Nomy.Rapyd.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Rapyd
{
    [RegisterExtension(typeof(IInitializer), Lifetime = LifetimeManagement.Singleton)]
    public class RapydTester : InitializerBase
    {
        private readonly IRapydApi _rapydApi;

        public RapydTester(IRapydApi rapydApi)
        {
            _rapydApi = rapydApi;
        }

        public override ExtensionOrderPriorities Priority => ExtensionOrderPriorities.Lowest;

        protected override async Task InitializeInner(CancellationToken cancellationToken)
        {
            //await GetWallet().ConfigureAwait(false);
            //await CreateWallet().ConfigureAwait(false);
        }

        private async Task GetWallet()
        {
            var response = await _rapydApi.GetWallet("ewallet_e1fd3d81f03b8e8f8e4c91df9d9af850").ConfigureAwait(false);
        }

        private async Task CreateWallet()
        {
            var response = await _rapydApi.CreateWallet(new WalletRequestDTO
            {
                Category = WalletCategory.General,
                Contact = new WalletContact
                {
                    ContactType = ContactType.Personal,
                    Country = "IL",
                    DateOfBirth = new DateTime(1980, 3, 9),
                    Email = "muaddibco@gmail.com",
                    FirstName = "Kirill",
                    LastName = "Gandyl"
                },
                FirstName = "Kirill",
                LastName = "Gandyl",
                EwalletReferenceId = "KirillGandyl2021061201",
                Type = WalletType.Person
            }).ConfigureAwait(false);
        }
    }
}
