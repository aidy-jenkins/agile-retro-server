
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace AgileRetroServer.Repositories 
{

    public interface IItemRepository 
    {
        void AddItems(string roomCode, string category, IEnumerable<string> items);
        IEnumerable<string> GetItems(string roomCode, string category);
    }

    public class ItemRepository : IItemRepository
    {
        private static MemoryCache _cache = new MemoryCache("ItemCache");

        private string GetKey(string roomCode, string category)
            => (roomCode, category).GetHashCode().ToString();

        public IEnumerable<string> GetItems(string roomCode, string category)
            => _cache[GetKey(roomCode, category)] as IEnumerable<string> ?? Enumerable.Empty<string>();

        public void AddItems(string roomCode, string category, IEnumerable<string> items)
        {
            var key = GetKey(roomCode, category);
            lock(key) 
            {
                _cache[key] = (_cache[key] as IEnumerable<string> ?? Enumerable.Empty<string>()).Concat(items);
            }
        }
    }
}