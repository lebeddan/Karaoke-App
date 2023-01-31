using Karaoke_Proj.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Karaoke_Proj.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
		LoginViewModel loginView = new LoginViewModel();
		public LoginView ()
		{
			InitializeComponent ();
			BindingContext= loginView;
			loginView.DisplayLoginAlert += () => DisplayAlert("Warning", "Default user!", "OK");
		}
	}
}