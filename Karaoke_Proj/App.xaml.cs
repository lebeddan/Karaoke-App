using Karaoke_Proj.Helpers;
using Karaoke_Proj.Services;
using Karaoke_Proj.ViewModels;
using MediaManager;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Karaoke_Proj
{
    public partial class App : Application
    {
        private MainViewModel viewModel = new MainViewModel();
        public App()
        {
            TheTheme.SetTheme();
            // Init music player manager
            CrossMediaManager.Current.Init();
            // Register dependecies
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockFavoriteDataStore>();

            BindingContext = viewModel;
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            OnResume();
        }

        protected override void OnSleep()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TheTheme.SetTheme();
            });
        }
    }
}
