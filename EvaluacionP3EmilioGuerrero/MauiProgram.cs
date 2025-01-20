using EvaluacionP3EmilioGuerrero.Repositories;
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
            string dbPath = FileAccessHelper.GetLocalFilePath("pais.db3");
            builder.Services.AddSingleton<PaisesRepository>(s => ActivatorUtilities.CreateInstance<PaisesRepository>(s, dbPath));


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
