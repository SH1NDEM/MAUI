namespace MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            AppSettings.ApplyToResources(Resources);
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
