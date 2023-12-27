using Exchnage.Library.ClinetHttpServices;
using Microsoft.Extensions.Logging;

namespace Exchange.Mobile
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
                });

            builder.Services.AddMauiBlazorWebView();



              var BaseAddress =
    DeviceInfo.Platform == DevicePlatform.Android ? "https://90j8m7ws-7271.use.devtunnels.ms" : "http://localhost:7271";
         var APIUrl = $"{BaseAddress}/api/";


            builder.Services.AddHttpClient();

            builder.Services.AddHttpClient<ApplicationHttpClient>(client =>
            {
                client.BaseAddress = new Uri(APIUrl);
            });
            builder.Services.AddBlazorWebViewDeveloperTools();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
