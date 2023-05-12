using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Imtua.UtilityBot;
using Imtua.UtilityBot.Controllers;
using Imtua.UtilityBot.Services;
using Imtua.UtilityBot.Configurations;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.Unicode;
        var host = new HostBuilder().ConfigureServices((HostContext, services) => ConfigureServices(services))
            .UseConsoleLifetime().Build();
        Console.WriteLine("Сервис запущен");
        await host.RunAsync();
        Console.WriteLine("Бот запущен");
    }

    static void ConfigureServices(IServiceCollection services)
    {
        var appSettings = BuildAppSettings();
        services.AddSingleton(BuildAppSettings);

        services.AddTransient<DefaultMessageController>();
        services.AddTransient<InlineKeyboardController>();
        services.AddTransient<TextMessageController>();

        services.AddSingleton<IStorage, MemoryStorage>();

        services.AddSingleton<ITelegramBotClient>(provider =>
            new TelegramBotClient(appSettings.BotToken));
        services.AddHostedService<Bot>();
    }
    
    static AppSettings BuildAppSettings()
    {
        return new AppSettings
        {
            BotToken = "6164423544:AAGfePpzhaBd5kwATtSVpYXHJwb0_Xpccr4"
        };
    }
}