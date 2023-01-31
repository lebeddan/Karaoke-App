using MediaManager;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.AudioRecorder;
using Session = Karaoke_Proj.Models.Session;

/// <summary>
///  This class is contains song info, play selsected song, start recording, share recording and set to favorite song.
/// </summary>

namespace Karaoke_Proj.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        private Song _selectedSong;
        private ObservableCollection<Song> _songList;

        private TimeSpan _duration;
        private TimeSpan _position;

        private double _max = 100f;

        private bool _isPlaying;

        private bool _isRecording;

        private bool _isFavorite;

        // AudioPlayer that play songs
        private IMediaManager mediaManager = CrossMediaManager.Current;

        // Record properties
        private readonly AudioRecorderService audioRecorderService = new AudioRecorderService();
        // AudioPlayer to play recorded session
        private readonly AudioPlayer audioPlayer = new AudioPlayer();


        public PlayerViewModel(Song selectedSong, ObservableCollection<Song> songList)
        {
            _selectedSong = selectedSong;
            _songList = songList;
            IsFavorite = false;
            PlaySong(selectedSong);
        }

        /// <summary>
        ///  Async handler to set position of the slider same with position of the song.
        /// </summary>
        private async void Current_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        {
            Position = mediaManager.Position;
            if (Position.TotalSeconds == Max)
            {
                IsPlaying = false;
                await mediaManager.Stop();
                await mediaManager.SeekToStart();
            }
        }

        /// <summary>
        ///  Observable collection that contains songs.(not used)
        /// </summary>
        public ObservableCollection<Song> SongList
        {
            get { return _songList; }
            set { SetProperty(ref _songList, value); }
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
        ///  Property that contains duration of the song.
        /// </summary>
        public TimeSpan Duration
        {
            get { return _duration; }
            set { SetProperty(ref _duration, value); }
        }

        /// <summary>
        ///  Property that contains position of the song.
        /// </summary>
        public TimeSpan Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }

        /// <summary>
        ///  Property that contains max duration of the song.
        /// </summary>
        public double Max
        {
            get { return _max; }
            set { 
                if (value > 0)
                {
                    SetProperty(ref _max, value);
                }
            }
        }

        /// <summary>
        ///  Property to track if song is playing or not.
        ///  Also change play icon.
        /// </summary>
        public bool IsPlaying
        {
            get { return _isPlaying; }
            set { SetProperty(ref _isPlaying, value); OnPropertyChanged(nameof(PlayIcon)); }
        }

        /// <summary>
        ///  Property to track if session is recording or not.
        ///  Also change record icon.
        /// </summary>
        public bool IsRecording
        {
            get { return _isRecording; }
            set { SetProperty(ref _isRecording, value); OnPropertyChanged(nameof(RecordIcon)); }
        }

        /// <summary>
        ///  Property to track if song is favorite or not.
        ///  Also change favorite icon.
        /// </summary>
        public bool IsFavorite
        {
            get { return _isFavorite; }
            set { SetProperty(ref _isFavorite, value); OnPropertyChanged(nameof(FavoriteIcon)); }
        }

        /// <summary>
        ///  Property that contains path to play icon and change depending 
        ///  on property IsPlaying.
        /// </summary>
        public string PlayIcon
        {
            get => IsPlaying ? "ic_pause36.png" : "ic_play36.png";
        }

        /// <summary>
        ///  Property that contains path to record icon and change depending 
        ///  on property IsRecording.
        /// </summary>
        public string RecordIcon
        {
            get => IsRecording ? "ic_stop36.png" : "ic_record36.png";
        }

        /// <summary>
        ///  Property that contains path to favorite icon and change depending 
        ///  on property IsFavorite.
        /// </summary>
        public string FavoriteIcon
        {
            get => IsFavorite ? "ic_favorite24.png" : "ic_favorite_outlined24.png";
        }

        /// <summary>
        ///  Command to start play selected song.
        /// </summary>
        public ICommand PlayCommand => new Command(Play);

        /// <summary>
        ///  Command to share recorded session.
        /// </summary>
        public ICommand ShareCommand => new Command(ShareSession);

        /// <summary>
        ///  Command to return back in main view.
        /// </summary>
        public ICommand BackCommand => new Command(() => Application.Current.MainPage.Navigation.PopAsync());

        /// <summary>
        ///  Command to add/delete selected song to/from favorite songs.
        /// </summary>
        public ICommand FavoriteCommand => new Command(AddToFavorite);

        /// <summary>
        ///  Command to return back in main view.
        /// </summary>
        public ICommand CloseCommand => new Command(Close);

        /// <summary>
        ///  Command to start record a session.
        /// </summary>
        public ICommand RecordCommand => new Command(RecordSession);


        private async void AddToFavorite()
        {
            if (!IsFavorite)
            {
                IsFavorite = true;
                // Add item asynchronic
                await SongDataStore.AddItemAsync(SelectedSong);
            }
            else
            {
                IsFavorite = false;
                // Delete item asynchronic
                await SongDataStore.DeleteItemAsync(SelectedSong);
            }
        }

        private async void ShareSession()
        {
            if (IsRecording)
            {
                // Stop recording and playing song
                IsRecording = false;
                IsPlaying = false;
                await mediaManager.Stop();
                await audioRecorderService.StopRecording();

                // Invoke android share and share recorded .wav file
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = audioRecorderService.GetAudioFilePath(),
                    File = new ShareFile(audioRecorderService.GetAudioFilePath())
                });
            }
            else 
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Title = "Check my results!",
                    Text = _selectedSong.SongName + " " + _selectedSong.SongAuthor
                });
            }
        }

        private string recorded; // global varibale to contain a recorded session
        private async void RecordSession()
        {
            if (audioRecorderService.IsRecording)
            {
                // Invoke a alert message when recording
                var response = await App.Current.MainPage.DisplayAlert(
                    "Warning", 
                    "Are you sure you want to interrupt the recording?", 
                    "Yes", "No"
                );

                // If resoponse is "yes" => stop recording and save it
                if (response == true)
                {
                    IsRecording = false;
                    await audioRecorderService.StopRecording();
                    recorded = audioRecorderService.GetAudioFilePath();
                    //audioPlayer.Play(audioRecorderService.GetAudioFilePath()); Debug
                }
            }
            else
            {
                IsRecording = true;
                await audioRecorderService.StartRecording();
            }
        }

        private async void Close()
        {
            // Invoke a alert message when close a music player
            var response = await App.Current.MainPage.DisplayAlert(
                    "Warning",
                    "Are you sure you want to interrupt the recording?",
                    "Yes", "No"
            );

            // If resoponse is "yes" => stop recording, playing and save it
            if (response == true)
            {
                await mediaManager.Pause();
                IsPlaying = false;
                // If when close Is recording => stop recording and save it
                if (audioRecorderService.IsRecording)
                {
                    IsRecording = false;
                    await audioRecorderService.StopRecording();

                    // Create a Session object and add it asyncroniclly 
                    Session newSession = new Session(audioRecorderService.GetAudioFilePath());
                    await DataStore.AddItemAsync(newSession);
                    // Close a music player
                    await Shell.Current.GoToAsync("..");
                }
                // Not => check if user already recorded a session
                else
                {
                    if (recorded != null)
                    {
                        Session newSession = new Session(recorded);
                        await DataStore.AddItemAsync(newSession);
                    }
                    await Shell.Current.GoToAsync("..");
                }
            }
        }

        private async void Play()
        {
            if (IsPlaying)
            {
                await mediaManager.Pause();
                IsPlaying= false;
            }
            else
            {
                await mediaManager.Play();
                IsPlaying = true;
            }
        }

        private async void PlaySong(Song song)
        {
            // Play song form the URL link 
            await mediaManager.Play(song?.URL);
            IsPlaying = true;

            mediaManager.MediaItemFinished += (sender, args) =>
            {
                IsPlaying= false;
            };

            // Set duration, max and position properties from song 
            Device.StartTimer(TimeSpan.FromMilliseconds(300), () =>
            {
                Duration = mediaManager.Duration;
                Max = _duration.TotalSeconds;
                mediaManager.PositionChanged += Current_PositionChanged;
                return true;
            });
        }
    }
}
