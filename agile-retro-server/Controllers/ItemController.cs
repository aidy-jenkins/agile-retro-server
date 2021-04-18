using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileRetroServer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AgileRetroServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private IItemRepository _itemRepository;
        
        public ItemController(IItemRepository itemRepository, ILogger<CategoryController> logger) 
            => (_itemRepository, _logger) = (itemRepository, logger);

        [HttpGet("/room/{roomCode}/category/{category}/items")]
        public IEnumerable<string> GetItems(string roomCode, string category)
        {
            return _itemRepository.GetItems(roomCode, category);
        }

        [HttpPost("/room/{roomCode}/category/{category}/items")]
        public void UpdateItems(string roomCode, string category, [FromBody] AddItemsRequest request)
        {
            _itemRepository.AddItems(roomCode, category, request.Items);
        }

        public class AddItemsRequest
        {
            public IEnumerable<string> Items {get; set;}
        }
    }
}
