using BreakFastAPI.Interfaces;

using BreakfastClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace BreakFastAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreakfastController : ControllerBase
    {
        private readonly IBreakfastRepository _breakfastRepository;
        public BreakfastController(IBreakfastRepository breakfastRepository)
        {
            _breakfastRepository = breakfastRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBreakfast([FromBody] List<Ingredient> ingredients)
        {
            var _httpClient = new HttpClient();

            // sending a POST request to the inventory Web API that is not implemented yet
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7268/api/Inventory/check-availability", ingredients);

            if (!response.IsSuccessStatusCode)
            {
                return Ok(new { Message = " unsuccessfull status code!" });
            }

            var availability = await response.Content.ReadFromJsonAsync<bool>();

            if (availability)
            {
                // create the breakfast
                var breakfast = new BreakfastRequest()
                {
                    bId = _breakfastRepository.GetAllBreakfasts().Count() + 1,
                    Ingredients = ingredients
                };

                _breakfastRepository.AddBreakfast(breakfast);

                return Ok(new { Message = "Breakfast created successfully!" });
            }
            return BadRequest(new { Message = "Not enough ingredients to create breakfast." });

        }
        // Get all breakfasts
        [HttpGet]
        public IEnumerable<BreakfastRequest> GetAllBreakfasts()
        {
            return _breakfastRepository.GetAllBreakfasts();
        }
    }
}
