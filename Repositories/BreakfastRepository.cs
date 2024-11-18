using BreakFastAPI.Interfaces;
using BreakfastClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace BreakFastAPI.Repositories
{
    public class BreakfastRepository : IBreakfastRepository
    {

        private readonly List<BreakfastRequest> _breakfasts = new List<BreakfastRequest>();   //new();

        public BreakfastRepository()
        {
            var ingredient1 = new Ingredient() { Name = "bread", Quantity = 1 };
            var ingredient2 = new Ingredient() { Name = "cheese", Quantity = 1 };
            var ingredients= new List<Ingredient>() {  ingredient1, ingredient2 };
            _breakfasts.Add(new BreakfastRequest() { bId = 1, Ingredients = ingredients });         
        }

        // Get all breakfasts
        public IEnumerable<BreakfastRequest> GetAllBreakfasts()
        {
            return _breakfasts;
        }

        
        // Get breakfast by id
        public BreakfastRequest GetBreakfastById(int id)
        {
             return _breakfasts.Find(u => u.bId == id);       
        }

        // Add a new breakfast
        public void AddBreakfast(BreakfastRequest breakfast)
        {
            breakfast.bId = _breakfasts.Any() ? _breakfasts.Max(u => u.bId) + 1 : 1;
            _breakfasts.Add(breakfast);
        }
       
        // Delete Breakfast
        public bool DeleteBreakfast(int id)
        {
            var breakfast = GetBreakfastById(id);
            if (breakfast == null)
            {
                return false;
            }
            _breakfasts.Remove(breakfast);
            return true;
        }       
    }
}
