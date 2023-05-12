using Telegram.Bot;
using Telegram.Bot.Types;

namespace Imtua.UtilityBot.Controllers
{
    internal class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramClient)
        {
            _telegramClient = telegramClient;
        }

        public async Task Handle(CallbackQuery? callBackQuery, CancellationToken ct)
        {
            Console.WriteLine($"Нажатие на кнопку от пользователя @{callBackQuery.From.Username}");
            await _telegramClient.SendTextMessageAsync(callBackQuery.From.Id, $"Вы нажали на кнопку", cancellationToken: ct);
        }
    }
}
