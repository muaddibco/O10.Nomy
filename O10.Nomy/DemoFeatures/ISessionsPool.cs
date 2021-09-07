using LanguageExt;
using O10.Core.Architecture;
using O10.Nomy.DemoFeatures.Models;

namespace O10.Nomy.DemoFeatures
{
    [ServiceContract]
    public interface ISessionsPool
    {
        void Push(UserSessionInfo userSessionInfo);
        Option<UserSessionInfo> Pull(string sessionKey);
    }
}
