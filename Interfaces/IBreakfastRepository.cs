

using BreakfastClassLibrary.Models;

namespace BreakFastAPI.Interfaces
{

    public interface IBreakfastRepository
    {
        public IEnumerable<BreakfastRequest> GetAllBreakfasts();
        public BreakfastRequest GetBreakfastById(int id);
        public bool DeleteBreakfast(int id);
        public void AddBreakfast(BreakfastRequest breakfast);
    }
}