using Karaoke_Proj.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Karaoke_Proj.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SongListView : ContentPage
    {
        MainViewModel mainView;
        public SongListView()
        {
            InitializeComponent();
            BindingContext= mainView = new MainViewModel();
            ItemsListView.ItemsSource = mainView.GetSearchResults(searchBar.Text); 
        }

        /// <summary>
        ///  Method to set music list to seaarched results.
        /// </summary>
        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ItemsListView.ItemsSource = mainView.GetSearchResults(e.NewTextValue);
        }
    }
}