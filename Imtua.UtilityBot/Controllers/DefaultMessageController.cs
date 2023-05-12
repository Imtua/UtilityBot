using Telegram.Bot;
using Telegram.Bot.Types;

namespace Imtua.UtilityBot.Controllers
{
    internal class DefaultMessageController
    {
        private readonly ITelegramBotClient _telegramClient;

        public DefaultMessageController(ITelegramBotClient telegramClient)
        {
            _telegramClient = telegramClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Пользователь @{message.Chat.Username} пытается отправить сообщение неподдерживаемого типа");
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Неправильный ввод", cancellationToken: ct);
        }
    }
}
