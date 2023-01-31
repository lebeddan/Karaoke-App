using Karaoke_Proj.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Karaoke_Proj.Models;

/// <summary>
///  This class is a base view model that realize interface INotifyPropertyChanged.
/// </summary>
namespace Karaoke_Proj.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///  Dependecy property for recorded sessions to add, udpate and delete.
        /// </summary>
        public IDataStore<Session> DataStore => DependencyService.Get<IDataStore<Session>>();

        /// <summary>
        ///  Dependecy property for favorite songs to add, udpate and delete.
        /// </summary>
        public IDataStore<Song> SongDataStore => DependencyService.Get<IDataStore<Song>>();

        bool isBusy = false;

        /// <summary>
        ///  Property for manage update in RefreshView.
        /// </summary>
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        /// <summary>
        ///  Method to set value for properties.
        /// </summary>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        ///  Method to update properties.
        /// </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
