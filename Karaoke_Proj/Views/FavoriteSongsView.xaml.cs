using Karaoke_Proj.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Karaoke_Proj.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoriteSongsView : ContentPage
    {
        MainViewModel model = new MainViewModel();
        public FavoriteSongsView()
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}