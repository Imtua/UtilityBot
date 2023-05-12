using Imtua.UtilityBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Imtua.UtilityBot.Controllers
{
    internal class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public InlineKeyboardController(ITelegramBotClient telegramClient, IStorage memoryStorage)
        {
            _telegramClient = telegramClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callBackQuery, CancellationToken ct)
        {
            if (callBackQuery?.Data == null)
            {
                return;
            }
            _memoryStorage.GetSession(callBackQuery.From.Id).BotFunction = callBackQuery.Data;

            switch (callBackQuery.Data)
            {
                case "Length":
                    await _telegramClient.SendTextMessageAsync(callBackQuery.From.Id,
                        $"Отправьте сообщение для получения его длинны", cancellationToken: ct);
                    break;
                
                case "Sum":
                    await _telegramClient.SendTextMessageAsync(callBackQuery.From.Id,
                        $"Отправьте ряд чисел через пробел для получения его суммы", cancellationToken: ct);
                    break;
            }
        }
    }
}
