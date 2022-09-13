using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FoodMenu.Helpers;
using FoodMenu.Models;

namespace FoodMenu.Repositories
{
    public class MealsRepository : IMealsRepository
    {
        private const string GetMealByNameLink = @"https://www.themealdb.com/api/json/v1/1/search.php?s={0}";
        private const string GetMealByCategoryLink = @"https://www.themealdb.com/api/json/v1/1/filter.php?c={0}";
        private const string GetMealByAreaLink = @"https://www.themealdb.com/api/json/v1/1/filter.php?a={0}";

        private readonly HttpClient _client;

        public MealsRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<Meal?> GetMainMealDetails(string name)
        {
            var response = await GetResponseByLink(string.Format(GetMealByNameLink, name));

            var models = ResolveMealResponse(response);

            return models?.MealsList.FirstOrDefault();
        }

        public async Task<IEnumerable<string>> GetMealsByCategory(string category, int count)
        {
            var response = await GetResponseByLink(string.Format(GetMealByCategoryLink, category));

            var models = ResolveMealResponse(response);

            return models?.MealsList.Select(x => x?.Name).Take(count).ToList()!;
        }

        public async Task<IEnumerable<string>> GetMealsByArea(string area, int count)
        {
            var response = await GetResponseByLink(string.Format(GetMealByAreaLink, area));

            var models = ResolveMealResponse(response);

            return models?.MealsList.Select(x => x?.Name).Take(count).ToList()!;
        }

        private async Task<string> GetResponseByLink(string link)
        {
            return await _client.GetStringAsync(link);
        }

        private static Meals? ResolveMealResponse(string response)
        {
            if (response == "{\"meals\":null}")
            {
                return null;
            }

            var models = JsonSerializer.Deserialize<Meals>(
                response,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = new CustomNamingPolicy()
                });

            return models;
        }
    }
}