using Xamarin.Forms;

/// <summary>
///  This is a static class that set app set theme depend on user choice.
/// </summary>
/// 
namespace Karaoke_Proj.Helpers
{
    public static class TheTheme
    {
        public static void SetTheme()
        {
            switch(Settings.Theme)
            {
                case 0:
                    App.Current.UserAppTheme = OSAppTheme.Unspecified; // System theme
                    break;
                case 1:
                    App.Current.UserAppTheme = OSAppTheme.Dark;
                    break;
                case 2:
                    App.Current.UserAppTheme = OSAppTheme.Light;
                    break;
            }
        }
    }
}
