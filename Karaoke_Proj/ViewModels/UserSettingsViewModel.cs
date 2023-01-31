using Karaoke_Proj.Models;
using Karaoke_Proj.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

/// <summary>
///  This class is contains user data and user control for change music player appearance.
/// </summary>

namespace Karaoke_Proj.ViewModels
{
    [QueryProperty(nameof(Login), "login")]
    [QueryProperty(nameof(Password), "password")]
    public class UserSettingsViewModel : BaseViewModel
    {
        private ObservableCollection<MyColor> _colorList;
        private MyColor _selectedColor;
        private MyColor _selectedColor1;
        
        private string _login;
        private string _password;

        private int _textLyricsSize = 14;

        private string tempLogin = "Default";
        private string tempPassword = "Password";

        public UserSettingsViewModel()
        {
            _colorList = GetColors();
            // Set colors to default values
            SelectedColor = _colorList[0];
            SelectedColor1 = _colorList[8];
        }

        /// <summary>
        ///  Login property.
        /// </summary>
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        /// <summary>
        ///  Password property.
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value);}
        }

        /// <summary>
        ///  Property that contains color name and value.
        ///  Also change background color of the music player view.
        /// </summary>
        public MyColor SelectedColor
        {
            get { return _selectedColor; }
            set {
                App.Current.Resources["BackgroundPlayer"] = value.color.ToHex();
                SetProperty(ref _selectedColor, value); 
            }
        }

        /// <summary>
        ///  Property that contains color name and value.
        ///  Also change lyrics color of the music player view.
        /// </summary>
        public MyColor SelectedColor1
        {
            get { return _selectedColor1; }
            set
            {
                App.Current.Resources["LyricsColor"] = value.color.ToHex();
                SetProperty(ref _selectedColor1, value);
            }
        }

        /// <summary>
        ///  Observable collection that contains colors.
        /// </summary>
        public ObservableCollection<MyColor> ColorList
        {
            get { return _colorList; }
            set { SetProperty(ref _colorList, value); }
        }

        /// <summary>
        ///  Property that contains a number of lyrics size.
        /// </summary>
        public int TextLyricsSize
        {
            get { return _textLyricsSize; }
            set {
                App.Current.Resources["LyricsSize"] = value;
                SetProperty(ref _textLyricsSize, value); 
            }
        }

        private ObservableCollection<MyColor> GetColors()
        {
            return new ObservableCollection<MyColor>
            {
               new MyColor(){name="Default", color=Color.FromHex("#9B5151")},
               new MyColor(){name="Red", color=Color.Red},
               new MyColor(){name="Blue", color=Color.Blue},
               new MyColor(){name="Yellow", color=Color.Yellow},
               new MyColor(){name="Green", color=Color.Green},
               new MyColor(){name="Cyan", color=Color.Cyan},
               new MyColor(){name="Brown", color=Color.Brown},
               new MyColor(){name="Black", color=Color.Black},
               new MyColor(){name="White", color=Color.White},
               new MyColor(){name="Violet", color=Color.Violet}
            };
        }

        /// <summary>
        ///  Command for invoke view to change login data.
        /// </summary>
        public ICommand ChangeCommand => new Command(ChangeLoginData);

        /// <summary>
        ///  Command for save changes of login data.
        /// </summary>
        public ICommand SaveCommand => new Command(Save);

        /// <summary>
        ///  Command for discard changes of login data.
        /// </summary>
        public ICommand CancelCommand => new Command(Cancel);

        private async void Save()
        {
            // Save new login data in temp variables
            tempLogin = Login;
            tempPassword= Password;
            // Return to UserInfoView with new data
            await Shell.Current.GoToAsync($"..?login={Login}&password={Password}");
        }

        private async void Cancel()
        {
            await Shell.Current.GoToAsync($"..?login={tempLogin}&password={tempPassword}");
        }

        private async void ChangeLoginData()
        {
            // Binding context for UserDataView
            var userDataPage = new UserDataView { BindingContext = this };
            var navigation = Application.Current.MainPage.Navigation;
            // Open pop-up window
            await navigation.PushModalAsync(userDataPage, true);
        }
    }
}
