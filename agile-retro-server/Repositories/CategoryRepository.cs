using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace AgileRetroServer.Repositories 
{

    public interface ICategoryRepository 
    {
        void AddCategories(string roomCode, IEnumerable<string> categories);
        IEnumerable<string> GetCategories(string roomCode);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private static MemoryCache _cache = new MemoryCache("CategoryCache");

        public IEnumerable<string> GetCategories(string roomCode)
            => _cache[roomCode] as IEnumerable<string> ?? Enumerable.Empty<string>();

        public void AddCategories(string roomCode, IEnumerable<string> categories)
        {
            lock(roomCode) 
            {
                _cache[roomCode] = (_cache[roomCode] as IEnumerable<string> ?? Enumerable.Empty<string>()).Union(categories);
            }
        }
    }
}