using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
///  The interface for objects that need to interact with a data store.
/// </summary>

namespace Karaoke_Proj.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(T item);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
