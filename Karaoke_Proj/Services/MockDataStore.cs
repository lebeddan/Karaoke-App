using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Session = Karaoke_Proj.Models.Session;

/// <summary>
///  This is class that realize interface IDataStore to manage recorded sessions.
/// </summary>

namespace Karaoke_Proj.Services
{
    public class MockDataStore : IDataStore<Session>
    {
        readonly List<Session> _sessions;

        public MockDataStore()
        {
            _sessions = new List<Session>();
        }

        /// <summary>
        ///  Add items to list asyncronically.
        /// </summary>
        public async Task<bool> AddItemAsync(Session item)
        {
            _sessions.Add(item);
            return await Task.FromResult(true);
        }

        /// <summary>
        ///  Delete items to list asyncronically.
        /// </summary>
        public async Task<bool> DeleteItemAsync(Session item)
        {
            if (_sessions.Contains(item))
            {
                _sessions.Remove(item);
            }
            return await Task.FromResult(true);
        }

        /// <summary>
        ///  Get items to list asyncronically with specifed item name.
        /// </summary>
        public async Task<Session> GetItemAsync(string name)
        {
            return await Task.FromResult(_sessions.FirstOrDefault(s => s.Name == name));
        }

        /// <summary>
        ///  Get items to list asyncronically.
        /// </summary>
        public async Task<IEnumerable<Session>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_sessions);
        }

        /// <summary>
        ///  Update items to list asyncronically with specified item.
        /// </summary>
        public async Task<bool> UpdateItemAsync(Session item)
        {
            var oldItem = _sessions.Where((Session arg) => arg.Name == item.Name).FirstOrDefault();
            _sessions.Remove(oldItem);
            _sessions.Add(item);

            return await Task.FromResult(true);
        }
    }
}
