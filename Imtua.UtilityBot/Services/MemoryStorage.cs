using Imtua.UtilityBot.Models;
using System.Collections.Concurrent;

namespace Imtua.UtilityBot.Services
{
    public class MemoryStorage : IStorage
    {
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions= new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long sessionId)
        {
            if (_sessions.ContainsKey(sessionId))
            {
                return _sessions[sessionId];
            }

            var newSession = new Session() { BotFunction = "Not selected" };
            _sessions.TryAdd(sessionId, newSession);
            return newSession;
        }
        public void ClearSession()
        {
            _sessions.Clear();
        }
    }
}
