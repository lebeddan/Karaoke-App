using MediaManager;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Karaoke_Proj.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MusicPlayerView : ContentPage
    { 
        public MusicPlayerView()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  Method that set song position when slider drag completed.
        /// </summary>
        private void Slider_DragCompleted(object sender, EventArgs e)
        {
            var slider = sender as Slider;
            CrossMediaManager.Current.SeekTo(TimeSpan.FromSeconds(slider.Value));
        } 
    }
}