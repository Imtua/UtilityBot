using Imtua.UtilityBot.Models;
using Imtua.UtilityBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Imtua.UtilityBot.Controllers
{
    internal class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramClient, IStorage memoryStorage)
        {
            _telegramClient = telegramClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            if (message.Text == "/choose")
            {
                await ShowStartMessage(message, ct);
            }
            else
            {
                var function = _memoryStorage.GetSession(message.Chat.Id).BotFunction;
                switch (function)
                {
                    case "Length":
                        await _telegramClient.SendTextMessageAsync(message.From.Id, $"Количество символов в сообщении {message.Text.Length}", cancellationToken: ct);
                        _memoryStorage.ClearSession();
                        break;

                    case "Sum":
                        var values = message.Text.Split(' ').ToArray();
                        List<int> ints = new List<int>();
                        foreach (var value in values)
                        {
                            int.TryParse(value, out int result);
                            if (result != 0)
                                ints.Add(result);
                        }
                        await _telegramClient.SendTextMessageAsync(message.From.Id, $"Сумма ряда чисел равна {ints.Sum()}", cancellationToken: ct);
                        _memoryStorage.ClearSession();
                        break;

                    case "Not selected":
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, $" 🤔 Выберите команду бота", cancellationToken: ct);
                        await ShowStartMessage(message, ct);
                        break;
                }
            }
        }

        private async Task ShowStartMessage(Message message, CancellationToken ct)
        {
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
        }
    }
}

