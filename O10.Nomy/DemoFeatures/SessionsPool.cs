using LanguageExt;
using O10.Core.Architecture;
using O10.Nomy.DemoFeatures.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace O10.Nomy.DemoFeatures
{
    [RegisterDefaultImplementation(typeof(ISessionsPool), Lifetime = LifetimeManagement.Singleton)]
    public class SessionsPool : ISessionsPool
    {
        private readonly ConcurrentDictionary<string, UserSessionInfo> _userSessions = new();

        public Option<UserSessionInfo> Pull(string sessionKey)
        {
            if(_userSessions.Remove(sessionKey, out var sessionInfo))
            {
                return sessionInfo;
            }

            return null;
        }

        public void Push(UserSessionInfo userSessionInfo)
        {
            _userSessions.AddOrUpdate(userSessionInfo.SessionKey, userSessionInfo, (k, v) => userSessionInfo);
        }
    }
}
