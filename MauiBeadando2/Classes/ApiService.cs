using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiBeadando2.Classes {
    public static class ApiService<T> where T : class {
        public static async Task<ObservableCollection<Part>> GetItemsAsync(PartCategoryEnum category, int page = 1, string searchterm = "") {
            var data = await ApiService<Part>.Get("", $"&part_cat_id={(int)category}&page_size=50&page={page}{(searchterm != "" ? $"&search={searchterm}" : "")}");
            return new ObservableCollection<Part>(data);
        }
        public static async Task<ObservableCollection<Set>> GetSetsAsync(Part part) {
            var colorData = await ApiService<PartColor>.Get($"{part.part_num}/colors/");
            ObservableCollection<Set> result = new();
            foreach (PartColor color in colorData) {
                var data = await ApiService<Set>.Get($"{part.part_num}/colors/{color.color_id}/sets/");
                result = new(result.Concat(data));
            }
            return result;
        }
        private async static Task<List<T>> Get(string endpoint, string param = "") {
            var _httpClient = new HttpClient();
            string _apiKey = await SecureStorage.GetAsync("API_KEY");
            string url = "https://rebrickable.com/api/v3/lego/parts/";
            try {
                var response = await _httpClient.GetAsync($"{url}{endpoint}?key={_apiKey}{param}");
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<Response<T>>(
                    json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return data.Results;
            }
            catch {
                return new List<T>();
            }
        }

        //https://rebrickable.com/api/v3/swagger/?key=52ceebc4d805283cb30c451d693e2716#!/lego/lego_sets_parts_list
    }
    public class Response<T> where T : class {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<T> Results { get; set; }
    }
}
