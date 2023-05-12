using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Imtua.UtilityBot;
using Imtua.UtilityBot.Controllers;

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
        services.AddTransient<DefaultMessageController>();
        services.AddTransient<InlineKeyboardController>();
        services.AddTransient<TextMessageController>();

        services.AddSingleton<ITelegramBotClient>(provider =>
            new TelegramBotClient("6164423544:AAGfePpzhaBd5kwATtSVpYXHJwb0_Xpccr4"));
        services.AddHostedService<Bot>();
    }
}