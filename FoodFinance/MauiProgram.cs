using Microsoft.Extensions.Logging;
using FoodFinance.Views;
using FoodFinance.ViewModels;

namespace FoodFinance
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Registrar ViewModels
            builder.Services.AddTransient<DayEntryViewModel>();
            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<RevenueViewModel>();

            // Registrar Pages
            builder.Services.AddTransient<DayEntryPage>();
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<RevenuePage>();

#if DEBUG
            builder.Logging.AddDebug();
            
            // Executar testes em debug
            System.Threading.Tasks.Task.Run(() =>
            {
                System.Threading.Thread.Sleep(2000); // Aguardar inicialização
                Tests.RevenueCalculatorTests.RunTests();
            });
#endif

            return builder.Build();
        }
    }
}
