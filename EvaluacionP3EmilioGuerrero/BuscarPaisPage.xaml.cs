using EvaluacionP3EmilioGuerrero.ViewModels;

namespace EvaluacionP3EmilioGuerrero;

public partial class BuscarPaisPage : ContentPage
{
    public BuscarPaisPage(BuscarPaisViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}