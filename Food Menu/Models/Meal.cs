using System.Collections.Generic;

namespace FoodMenu.Models
{
    public record Meal(
        string Name,
        string Category,
        string Area,
        IEnumerable<string> MealsByCategory,
        IEnumerable<string> MealsByArea);
}