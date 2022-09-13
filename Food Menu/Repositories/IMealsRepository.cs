using System.Collections.Generic;
using System.Threading.Tasks;
using FoodMenu.Models;

namespace FoodMenu.Repositories
{
    public interface IMealsRepository
    {
        Task<Meal?> GetMainMealDetails(string name);
        Task<IEnumerable<string>> GetMealsByCategory(string category, int count);
        Task<IEnumerable<string>> GetMealsByArea(string area, int count);
    }
}