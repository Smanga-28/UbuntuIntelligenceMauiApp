using Microsoft.Extensions.Logging;
using Ubuntu_Intelli_Compass.ViewModels;
using Ubuntu_Intelli_Compass.Views;

namespace Ubuntu_Intelli_Compass
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

#if DEBUG
    		builder.Logging.AddDebug();

#endif
            builder.Services.AddTransient<PreLoadPage>();
            builder.Services.AddTransient<PreLoadViewModel>();
            builder.Services.AddSingleton<AppShell>();

            return builder.Build();
        }
    }
}
