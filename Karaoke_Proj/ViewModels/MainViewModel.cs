using Karaoke_Proj.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Karaoke_Proj.Models;
using System.Threading.Tasks;
using System.Diagnostics;
using Plugin.AudioRecorder;

/// <summary>
///  This is a main class that manage songs items, favorite view, sessions view and share information to others view.
/// </summary>

namespace Karaoke_Proj.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<Song> _musicList;
        private ObservableCollection<Song> _favoriteSongsList;
        private Song _selectedSong;

        private ObservableCollection<Session> _sessions;
        private Session _session;

        public Command LoadItemsCommand { get; }
        public Command LoadSongsCommand { get; }

        private readonly AudioPlayer audioPlayer = new AudioPlayer();

        public MainViewModel()
        {
            _musicList = GetSongs();
            _sessions = new ObservableCollection<Session>();
            _favoriteSongsList = new ObservableCollection<Song>();

            // Load old and new added items in CollectionView
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadSongsCommand = new Command(async () => await ExecuteLoadSongsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            // Try to add new and old items in recorded sessions
            try
            {
                RecordedSessions.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    RecordedSessions.Add(item);
                    // Set selected session to first
                    RecordedSession = RecordedSessions.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoadSongsCommand()
        {
            IsBusy = true;
            // Try to add new and old items in favorite songs
            try
            {
                FavoriteSongsList.Clear();
                var items = await SongDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    FavoriteSongsList.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        ///  Observable collection that contains recorded sessions.
        /// </summary>
        public ObservableCollection<Session> RecordedSessions
        {
            get { return _sessions; }
            set { SetProperty(ref _sessions, value); }
        }

        /// <summary>
        ///  Recorded session that contains name of session.
        /// </summary>
        public Session RecordedSession
        {
            get { 
                return _session; 
            }
            set { SetProperty(ref _session, value);}
        }

        /// <summary>
        ///  Observable collection that contains songs.
        /// </summary>
        public ObservableCollection<Song> MusicList
        {
            get { return _musicList; }
            set { SetProperty(ref _musicList, value); }  
        }

        /// <summary>
        ///  Observable collection that contains favorite songs.
        /// </summary>
        public ObservableCollection<Song> FavoriteSongsList
        {
            get { return _favoriteSongsList; }
            set { SetProperty(ref _favoriteSongsList, value); }
        }

        /// <summary>
        ///  Seleceted song that contains title, author, url and lyrics of the song.
        /// </summary>
        public Song SelectedSong
        {
            get { return _selectedSong; }
            set { SetProperty(ref _selectedSong, value); }
        }

        /// <summary>
        ///  Command that send selected song to MusicPlayerView.
        /// </summary>
        public ICommand SelectionCommand => new Command(PlaySong);

        /// <summary>
        ///  Command that play selected recorded session in SessionsView.
        /// </summary>
        public ICommand PlayCommand => new Command(PlaySession);

        /// <summary>
        ///  Command that delete selected recorded session in SessionsView.
        /// </summary>
        public ICommand DeleteCommand => new Command(DeleteSession);

        private void PlaySong() 
        {
            if (_selectedSong != null)
            {
                // Send selected song in view model
                var viewModel = new PlayerViewModel(_selectedSong, _musicList);
                // Set context for view
                var playerPage = new MusicPlayerView { BindingContext = viewModel };
                // Navigate to music player view
                var navigation = App.Current.MainPage.Navigation;
                // Opens music player view in pop-up window
                navigation.PushModalAsync(playerPage, true);
            }
        }

        private void PlaySession()
        {
            audioPlayer.Play(RecordedSession.Name);
        }

        private async void DeleteSession()
        {
            await DataStore.DeleteItemAsync(RecordedSession);
        }

        private ObservableCollection<Song> GetSongs()
        {
            return new ObservableCollection<Song>
            {
               new Song(
                   "Crazy Love", // Title
                   "Aaron Neville", // Author
                   "ic_micro", // Icon
                   "https://ia800605.us.archive.org/32/items/Mp3Playlist_555/AaronNeville-CrazyLove.mp3", // .mp3 URL
                   // Lyrics
                   "I can hear her heartbeat from a thousand miles\r\nAnd the heavens open up every time she smiles\r\nAnd when I come to her that is where I belong\r\nAnd I'm running through her like a rivers song\r\nShe gives me love, love, love, love, crazy love\r\nShe gives me love, love, love, love, crazy love\r\nShe's got a fine sense of humor when I'm feeling low down\r\nAnd I'm coming to her when the sun goes down\r\nTake away my troubles, take away my grief\r\nTake away my heartaches in the night like a thief\r\nShe gives me love, love, love, love, crazy love\r\nShe gives me love, love, love, love, crazy love\r\nAnd I need her in the daytime ( I need her)\r\nAnd I need her in the night ( I need her)\r\nAnd I want to throw my arms around her\r\nKiss and hug her, kiss and hug her tight\r\nWhen I'm returning from so far away\r\nGives me some sweet loving, brightens up my day\r\nAnd it makes me righteous and it makes me whole\r\nAnd it makes me mellow right down to my soul\r\nShe gives me love, love, love, love, crazy love\r\nShe gives me love, love, love, love, crazy love"),
               
               new Song(
                   "If I Could",
                   "Celine Dion",
                   "ic_micro",
                   "https://ia800605.us.archive.org/32/items/Mp3Playlist_555/CelineDion-IfICould.mp3",
                   "If I could\r\nI'd protect you from the sadness in your eyes\r\nGive you courage in a world of compromise\r\nYes, I would\r\nIf I could\r\nI would teach you all the things I've never learned\r\nAnd I'd help you cross the bridges that I've burned\r\nYes, I would\r\nIf I could\r\nI would try to shield your innocence from time\r\nBut the part of life I gave you isn't mine\r\nI've watched you grow\r\nSo I could let you go\r\nIf I could\r\nI would help you make it through the hungry years\r\nBut I know that I could never cry your tears\r\nBut I would\r\nIf I could\r\nYes, if I live\r\nIn a time and place where you don't wanna be\r\nYou don't have to walk along this road with me\r\nMy yesterday\r\nWon't have to be your way\r\nIf I knew\r\nI would've tried to change the world I brought you to\r\nAnd there isn't very much that I could do\r\nBut I would\r\nIf I could\r\nOh baby, I just wants to protect you\r\nAnd help my baby through the hungry years\r\nIt comes with part of me\r\nAnd if you ever, ever need\r\nSad shoulder to cry on\r\nI'm just someone to talk to\r\nI'll be there, I'll be there\r\nI didn't change your world\r\nBut I would\r\nIf I could..."),

               new Song(
                   "Home",
                   "Daughtry",
                   "ic_micro",
                   "https://ia800605.us.archive.org/32/items/Mp3Playlist_555/Daughtry-Homeacoustic.mp3",
                   "I'm staring out into the night\r\nTryin' to hide the pain\r\nI'm going to the place where love\r\nAnd feeling good don't ever cost a thing\r\nAnd the pain you feel's a different kind of pain\r\nWell, I'm going home\r\nBack to the place where I belong\r\nAnd where your love has always been enough for me\r\nI'm not running from\r\nNo, I think you got me all wrong\r\nI don't regret this life I chose for me\r\nBut these places and these faces are getting old\r\nSo I'm going home\r\nWell, I'm going home\r\nThe miles are getting longer, it seems\r\nThe closer I get to you\r\nI've not always been the best man or friend for you\r\nBut your love remains true, and I don't know why\r\nYou always seem to give me another try\r\nSo I'm going home\r\nBack to the place where I belong\r\nAnd where your love has always been enough for me\r\nI'm not running from\r\nNo, I think you got me all wrong\r\nI don't regret this life I chose for me\r\nBut these places and these faces are getting old\r\nBe careful what you wish for\r\n'Cause you just might get it all\r\nYou just might get it all\r\nAnd then some you don't want\r\nBe careful what you wish for\r\n'Cause you just might get it all\r\nYou just might get it all, yeah\r\nWell, I'm going home\r\nBack to the place where I belong\r\nAnd where your love has always been enough for me\r\nI'm not running from\r\nNo, I think you got me all wrong\r\nI don't regret this life I chose for me\r\nBut these places and these faces are getting old\r\nI said these places and these faces are getting old\r\nSo I'm going home\r\nI'm going home")
            };
        }

        /// <summary>
        ///  Method that filter search query in MusicList and return filtered list.
        /// </summary>
        public ObservableCollection<Song> GetSearchResults(string queryString)
        {
            var normalizedQuery = queryString?.ToLower() ?? "";
            // Filter by title 
            var filteredList = _musicList.Where(x => x.SongName.ToLowerInvariant().Contains(normalizedQuery)).ToList();
            ObservableCollection<Song> ret = new ObservableCollection<Song>(filteredList);
            return ret;
        }

        /// <summary>
        ///  Method that invoke on appering SessionView and FavoriteSongsView to refresh thoose views.
        /// </summary>
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
