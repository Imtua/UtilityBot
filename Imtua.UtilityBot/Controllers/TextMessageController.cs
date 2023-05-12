using Telegram.Bot;
using Telegram.Bot.Types;

namespace Imtua.UtilityBot.Controllers
{
    internal class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;

        public TextMessageController(ITelegramBotClient telegramClient)
        {
            _telegramClient = telegramClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Получено сообщение от пользователя #{message.Chat.Id} - {message.Chat.FirstName} {message.Chat.LastName}:\n" +
                $"'{message.Text}'");
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Получено сообщение", cancellationToken: ct);
        }
    }
}
