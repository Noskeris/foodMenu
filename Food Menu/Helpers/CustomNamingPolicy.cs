using System.Collections.Generic;
using System.Text.Json;
using FoodMenu.Models;

namespace FoodMenu.Helpers
{
    public class CustomNamingPolicy : JsonNamingPolicy
    {
        private readonly Dictionary<string, string> _nameMapping = new()
        {
            [nameof(Meal.Name)] = "strMeal",
            [nameof(Meal.Category)] = "strCategory",
            [nameof(Meal.Area)] = "strArea",
            [nameof(Meals.MealsList)] = "meals"
        };

        public override string ConvertName(string name)
        {
            return _nameMapping.GetValueOrDefault(name, name);
        }
    }
}