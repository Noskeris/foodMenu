using System.Threading.Tasks;
using FoodMenu.Models;
using FoodMenu.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodMenu.Controllers
{
    [Route("api/v1/meals/")]
    public class MealsController : ControllerBase
    {
        private readonly IMealsRepository _mealsRepository;

        public MealsController(IMealsRepository mealsRepository)
        {
            _mealsRepository = mealsRepository;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetMealInformation(string name)
        {
            var mealModel = await _mealsRepository.GetMainMealDetails(name);

            if (mealModel == null)
            {
                return NotFound($"Meal '{name}' not found!");
            }

            var mealsByCategory =
                await _mealsRepository.GetMealsByCategory(mealModel.Category, 5);

            var mealsByArea =
                await _mealsRepository.GetMealsByArea(mealModel.Area, 3);

            var meal = new Meal(
                mealModel.Name,
                mealModel.Category,
                mealModel.Area,
                mealsByCategory,
                mealsByArea);

            return Ok(meal);
        }
    }
}