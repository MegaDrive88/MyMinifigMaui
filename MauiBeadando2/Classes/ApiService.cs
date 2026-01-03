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
    public class ApiService {
        private readonly HttpClient _httpClient = new();
        private readonly string _apiKey = Task.Run(()=>SecureStorage.GetAsync("API_KEY")).Result!;
        private readonly string url = "https://rebrickable.com/api/v3/lego/parts/";
        public async Task<ObservableCollection<Part>> GetItemsAsync(PartCategoryEnum category, int page = 1, string searchterm = "") {
            var response = await _httpClient.GetAsync($"{url}?key={_apiKey}&part_cat_id={(int)category}&page_size=50&page={page}{(searchterm != "" ? $"&search={searchterm}" : "")}");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Response<Part>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return new(data.Results ?? []);
        }
        private async Task<ObservableCollection<Set>> GetSetsAsync(Part part) {
            var colorsResponse = await _httpClient.GetAsync($"{url}{part.part_num}/colors/?key={_apiKey}");
            colorsResponse.EnsureSuccessStatusCode();
            return new();
        }
        //get /api/v3/lego/parts/{part_num}/colors/
        //get /api/v3/lego/parts/{part_num}/colors/{color_id}/sets/

        //https://rebrickable.com/api/v3/swagger/?key=52ceebc4d805283cb30c451d693e2716#!/lego/lego_sets_parts_list
        //}
    }
    public class Response<T> where T : class {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<T> Results { get; set; }
    }
}
