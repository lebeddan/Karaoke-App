using Karaoke_Proj.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Karaoke_Proj.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SessionsView : ContentPage
	{
		MainViewModel model = new MainViewModel();
		public SessionsView ()
		{
			BindingContext= model;
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            model.OnAppearing();
        }
    }
}