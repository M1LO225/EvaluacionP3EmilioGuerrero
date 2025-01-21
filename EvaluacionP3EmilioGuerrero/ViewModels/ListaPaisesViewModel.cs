using CommunityToolkit.Mvvm.ComponentModel;
using EvaluacionP3EmilioGuerrero.Modelos;
using EvaluacionP3EmilioGuerrero.Repositories;

namespace EvaluacionP3EmilioGuerrero.ViewModels
{
    public partial class ListaPaisesViewModel : ObservableObject
    {
        private readonly IPaisRepository _paisRepository;

        [ObservableProperty]
        private List<Pais> paises;

        public ListaPaisesViewModel(IPaisRepository paisRepository)
        {
            _paisRepository = paisRepository;
            CargarPaises();
        }

        private async void CargarPaises()
        {
            paises = await _paisRepository.GetAllPaisesAsync();
        }
    }
}
