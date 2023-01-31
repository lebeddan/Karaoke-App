using Xamarin.Essentials;

/// <summary>
///  This is a static class to set or get theme.
/// </summary>

namespace Karaoke_Proj.Helpers
{
    public static class Settings
    {
        const int theme = 0; // Default theme

        public static int Theme
        {
            get => Preferences.Get(nameof(Theme), theme);
            set => Preferences.Set(nameof(Theme), value);       
        }
    }
}
