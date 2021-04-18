using System.Collections.Generic;
using System.Linq;

namespace AgileRetroServer.Repositories 
{
    public interface ICategoryRepository 
    {
        IEnumerable<string> GetCategories(string roomCode);
    }

    public class CategoryRepository : ICategoryRepository
    {
        public IEnumerable<string> GetCategories(string roomCode)
        {
            return Enumerable.Empty<string>();
        }
    }
}