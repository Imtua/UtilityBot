using Telegram.Bot;

namespace Imtua.UtilityBot
{
    internal class Bot
    {
        private ITelegramBotClient _telegramBotClient;

        public Bot(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }

    }
}
