namespace EvaluacionP3EmilioGuerrero;
    using EvaluacionP3EmilioGuerrero.Repositories;
using EvaluacionP3EmilioGuerrero.Service;
using EvaluacionP3EmilioGuerrero.ViewModels;

public partial class App : Application 
    {
        public App()
        {
            InitializeComponent();

            var services = new ServiceCollection();

            // Registra las dependencias
            services.AddSingleton<IPaisRepository, PaisRepository>();
            services.AddSingleton<ApiService>();
            services.AddSingleton<BuscarPaisViewModel>();
            services.AddSingleton<ListaPaisesViewModel>();

            

            // Inicializa la página de inicio con el contenedor DI
            var serviceProvider = services.BuildServiceProvider();
            MainPage = new BuscarPaisPage(serviceProvider.GetService<BuscarPaisViewModel>());

    }


}

