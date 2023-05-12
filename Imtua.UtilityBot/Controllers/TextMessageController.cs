using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

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
            switch (message.Text)
            {
                case "/choose":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Количество символов", "Length"),
                        InlineKeyboardButton.WithCallbackData($"Сумма чисел", "Sum")
                    });
                    
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b> 🧐 Этот бот выполняет две функции </b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Бот может посчитать количество символов в сообщении" +
                        $" {Environment.NewLine}Бот может вычислить сумму ряда, отправленных ему чисел{Environment.NewLine}",
                        cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;

                default:
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $" 🤔 Выберите команду бота", cancellationToken: ct);
                    break;
            }
        }
    }
}
