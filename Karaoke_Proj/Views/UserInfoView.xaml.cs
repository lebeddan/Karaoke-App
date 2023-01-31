using Karaoke_Proj.Helpers;
using Karaoke_Proj.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Settings = Karaoke_Proj.Helpers.Settings;

namespace Karaoke_Proj.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserInfoView : ContentPage
    {
        UserSettingsViewModel userSettings;

        public UserInfoView()
        {
            InitializeComponent();
            userSettings = new UserSettingsViewModel();
            BindingContext= userSettings;
            // Set to last theme when app was runned
            switch (Settings.Theme)
            {
                case 0:
                    SystemRadioBtn.IsChecked = true;
                    break;
                case 1:
                    NightRadioBtn.IsChecked = true;
                    break;
                case 2:
                    LightRadioBtn.IsChecked = true;
                    break;
            }
        }

        /// <summary>
        ///  Method to set theme when radio button was cnahged.
        /// </summary>
        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var val = (sender as RadioButton)?.Value as string;
            if (string.IsNullOrWhiteSpace(val))
                return;

            switch (val)
            {
                case "System":
                    Settings.Theme = 0;
                    break;
                case "Night":
                    Settings.Theme = 1;
                    break;
                case "Light":
                    Settings.Theme = 2;
                    break;
            }

            TheTheme.SetTheme();
        }
    }
}