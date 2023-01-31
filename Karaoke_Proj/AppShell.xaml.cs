using Karaoke_Proj.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Karaoke_Proj
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            // Register routes to navigate between pages
            Routing.RegisterRoute("login/loginPage", typeof(LoginView));
            Routing.RegisterRoute("home/songsPage", typeof(SongListView));
            Routing.RegisterRoute("home/settingsPage", typeof(UserInfoView));
            InitializeComponent();
        }
    }
}