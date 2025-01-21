using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EvaluacionP3EmilioGuerrero.Modelos;
using EvaluacionP3EmilioGuerrero.Repositories;
using EvaluacionP3EmilioGuerrero.Service;

using System.Threading.Tasks;

namespace EvaluacionP3EmilioGuerrero.ViewModels
{
    public partial class BuscarPaisViewModel : ObservableObject
    {
        private readonly IPaisRepository _paisRepository;
        private readonly ApiService _apiService;

        [ObservableProperty]
        private string nombrePais;

        [ObservableProperty]
        private string mensaje;

        public BuscarPaisViewModel(IPaisRepository paisRepository, ApiService apiService)
        {
            _paisRepository = paisRepository;
            _apiService = apiService;
        }

        [RelayCommand]
        public async Task BuscarPaisAsync()
        {
            try
            {
                var paisApi = await _apiService.GetPaisAsync(NombrePais);
                if (paisApi != null)
                {
                    var nombreOficial = paisApi.Name?.Official ?? "Nombre no disponible";
                    var region = paisApi.Region ?? "Región no disponible";
                    var googleMapsLink = paisApi.Maps?.GoogleMaps ?? "Enlace no disponible";

                    var pais = new Pais
                    {
                        NombreOficial = nombreOficial,
                        Region = region,
                        GoogleMapsLink = googleMapsLink,
                        NombreBD = "EGuerrero"
                    };

                    await _paisRepository.AddPaisAsync(pais);
                    Mensaje = $"País {pais.NombreOficial} guardado exitosamente.";
                }
                else
                {
                    Mensaje = "No se encontró ningún país con ese nombre.";
                }
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al buscar el país: {ex.Message}";
            }
        }

        [RelayCommand]
        public void Limpiar()
        {
            NombrePais = string.Empty;
            Mensaje = string.Empty;
        }
    }
}
