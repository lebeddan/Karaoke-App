using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
///  This is class that realize interface IDataStore to manage favorite songs.
/// </summary>
/// 
namespace Karaoke_Proj.Services
{
    public class MockFavoriteDataStore : IDataStore<Song>
    {
        readonly List<Song> songs;

        public MockFavoriteDataStore()
        {
            songs = new List<Song>();
        }

        /// <summary>
        ///  Add items to list asyncronically.
        /// </summary>
        public async Task<bool> AddItemAsync(Song item)
        {
            songs.Add(item);
            return await Task.FromResult(true);
        }

        /// <summary>
        ///  Delete items to list asyncronically.
        /// </summary>
        public async Task<bool> DeleteItemAsync(Song item)
        {
            if (songs.Contains(item))
            {
                songs.Remove(item);
            }
            return await Task.FromResult(true);
        }

        /// <summary>
        ///  Get items to list asyncronically with specifed item id.
        /// </summary>
        public async Task<Song> GetItemAsync(string id)
        {
            return await Task.FromResult(songs.FirstOrDefault(s => s.SongName == id));
        }

        /// <summary>
        ///  Get items to list asyncronically.
        /// </summary>
        public async Task<IEnumerable<Song>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(songs);
        }

        /// <summary>
        ///  Update items to list asyncronically with specified item.
        /// </summary>
        public async Task<bool> UpdateItemAsync(Song item)
        {
            var oldItem = songs.Where((Song arg) => arg.SongName == item.SongName).FirstOrDefault();
            songs.Remove(oldItem);
            songs.Add(item);

            return await Task.FromResult(true);
        }
    }
}
