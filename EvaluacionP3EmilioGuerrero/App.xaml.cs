namespace EvaluacionP3EmilioGuerrero;
    using EvaluacionP3EmilioGuerrero.Repositories;
    
    public partial class App : Application 
    {
        public App()
        {
            InitializeComponent();
        }

    protected override Window CreateWindow(IActivationState activationState)
    {
        return new Window(new AppShell());
    }

}

