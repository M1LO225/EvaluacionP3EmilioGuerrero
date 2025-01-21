using EvaluacionP3EmilioGuerrero.Repositories;
using EvaluacionP3EmilioGuerrero.Service;
using EvaluacionP3EmilioGuerrero.ViewModels;
using Microsoft.Extensions.Logging;

namespace EvaluacionP3EmilioGuerrero
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

            // Servicios
            builder.Services.AddSingleton<BDService>();
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<IPaisRepository, PaisRepository>();

            // ViewModels
            builder.Services.AddSingleton<BuscarPaisViewModel>();
            builder.Services.AddSingleton<ListaPaisesViewModel>();

            // Páginas
            builder.Services.AddSingleton<BuscarPaisPage>();
            builder.Services.AddSingleton<ListaPaisesPage>();

            return builder.Build();
        }

    }
    public class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            return System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);
        }
    }
}
