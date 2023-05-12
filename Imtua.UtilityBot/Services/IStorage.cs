using Imtua.UtilityBot.Models;

namespace Imtua.UtilityBot.Services
{
    public interface IStorage
    {
        Session GetSession(long chatId);
    }
}
