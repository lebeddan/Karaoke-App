using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

/// <summary>
///  This class is contains music list and manage SearchBar.
/// </summary>

namespace Karaoke_Proj.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        private ObservableCollection<Song> _searchResults;
        private MainViewModel mainView;
        public SearchViewModel()
        {
            mainView = new MainViewModel();
            _searchResults = mainView.MusicList;
        }

        /// <summary>
        /// Observable collection to store search results.
        /// </summary>
        public ObservableCollection<Song> SearchResults
        {
            get { return _searchResults; }
            set { SetProperty(ref _searchResults, value); }
        }

        /// <summary>
        /// Command to perform search.
        /// </summary>
        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            SearchResults = mainView.GetSearchResults(query);
        });
    }
}
