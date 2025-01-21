using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EvaluacionP3EmilioGuerrero.Modelos;
using EvaluacionP3EmilioGuerrero.Repositories;
using EvaluacionP3EmilioGuerrero.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                var paisApi = await _apiService.GetPaisAsync(nombrePais);
                if (paisApi != null)
                {
                    var pais = new Pais
                    {
                        NombreOficial = paisApi.Name.Official,
                        Region = paisApi.Region,
                        GoogleMapsLink = paisApi.Maps.GoogleMaps,
                        NombreBD = "EGuerrero"
                    };

                    await _paisRepository.AddPaisAsync(pais);
                    mensaje = $"País {pais.NombreOficial} guardado.";
                }
                else
                {
                    mensaje = "No se encontró el país.";
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}";
            }
        }
    }
}
