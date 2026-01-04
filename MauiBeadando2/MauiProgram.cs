using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MauiBeadando2.ViewModels;
using MauiBeadando2.Pages;
using FFImageLoading.Maui;
using MauiBeadando2.Database;

namespace MauiBeadando2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseFFImageLoading()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            SecureStorage.Default.SetAsync("API_KEY", "52ceebc4d805283cb30c451d693e2716");
            //if (File.Exists(DatabaseConstants.DatabasePath)) {
            //    File.Delete(DatabaseConstants.DatabasePath);
            //}
#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MinifigBuilderViewModel>();
            builder.Services.AddSingleton<MinifigBuilderPage>();
            builder.Services.AddSingleton<PartSelectorViewModel>();
            builder.Services.AddSingleton<PartSelectorPage>();
            builder.Services.AddSingleton<PartDetailsViewModel>();
            builder.Services.AddSingleton<PartDetailsPage>();
            return builder.Build();
        }
    }
}
