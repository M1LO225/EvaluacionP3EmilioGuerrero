namespace EvaluacionP3EmilioGuerrero
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.BuscaPaisViewModel();
        }

        
    }

}
