using Imtua.UtilityBot.Controllers;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Imtua.UtilityBot
{
    internal class Bot : BackgroundService
    {
        private ITelegramBotClient _telegramBotClient;
        private DefaultMessageController _defaultMessageController;
        private InlineKeyboardController _inlineKeyboardController;
        private TextMessageController _textMessageController;

        public Bot(ITelegramBotClient telegramBotClient,
            DefaultMessageController defaultMessageController,
            InlineKeyboardController inlineKeyboardController,
            TextMessageController textMessageController)
        {
            _telegramBotClient = telegramBotClient;
            _defaultMessageController = defaultMessageController;
            _inlineKeyboardController = inlineKeyboardController;
            _textMessageController = textMessageController;
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken ct)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                await _inlineKeyboardController.Handle(update.CallbackQuery, ct);
            }
            if (update.Type == UpdateType.Message)
            {
                if (update.Message.Type == MessageType.Text)
                    await _textMessageController.Handle(update.Message, ct);

                else
                    await _defaultMessageController.Handle(update.Message, ct);
            }
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken ct)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException 
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ =>exception.ToString()
            };
            Console.WriteLine(errorMessage);

            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _telegramBotClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync,
                new ReceiverOptions()
                { AllowedUpdates = { } }, cancellationToken: stoppingToken);
            Console.WriteLine("Бот запущен");
        }
    }
}
