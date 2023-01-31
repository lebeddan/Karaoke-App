using System;
using System.Windows.Input;
using Xamarin.Forms;

/// <summary>
///  This class is manage login functions and properties.
/// </summary>

namespace Karaoke_Proj.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _login;
        private string _password;

        public LoginViewModel()
        {
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
            set { SetProperty(ref _password, value); }
        }

        /// <summary>
        ///  Action for display warning text for unregistered user.
        /// </summary>
        public Action DisplayLoginAlert;

        /// <summary>
        ///  Command for submit registration.
        /// </summary>
        public ICommand SubmitCommand => new Command(OnSubmit); 
   

        private async void OnSubmit()
        {
            if (Login == null || Password == null)
            {
                Login = "Default";
                Password= "Password";
                DisplayLoginAlert();
            }
            // Navigate to user settings page and pass user data
            await Shell.Current.GoToAsync($"//home/settingsPage?login={Login}&password={Password}");
        }
    }
}
