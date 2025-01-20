using EvaluacionP3EmilioGuerrero.ViewModels;

namespace EvaluacionP3EmilioGuerrero;

public partial class PaisPage : ContentPage
{
    private PaisViewModel _viewModel;
    public PaisPage()
	{
		InitializeComponent();
        _viewModel = new PaisViewModel();
        BindingContext = _viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadUsersAsync();
        Console.WriteLine($"Users loaded: {_viewModel.Paises.Count}");
    }
}