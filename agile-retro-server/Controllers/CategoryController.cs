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
    public class CategoryController : ControllerBase
    {
        private readonly string[] DefaultCategories = new [] 
        {
            "Good things",
            "Bad things",
            "Room for improvement",
            "Takeaways"
        };

        private readonly ILogger<CategoryController> _logger;
        private ICategoryRepository _categoryRepository;
        
        public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> logger) 
            => (_categoryRepository, _logger) = (categoryRepository, logger);


        [HttpGet("/room/{roomCode}/categories")]
        public IEnumerable<string> GetCategories(string roomCode)
        {
            var categories = _categoryRepository.GetCategories(roomCode);
            if(categories.Any())
                return categories;

            return DefaultCategories;
        }

        [HttpPost("/room/{roomCode}/categories")]
        public void UpdateCategories(string roomCode, [FromBody] AddCategoriesRequest request)
        {
            _categoryRepository.AddCategories(roomCode, request.Categories);
        }

        public class AddCategoriesRequest
        {
            public IEnumerable<string> Categories {get; set;}
        }
    }
}
