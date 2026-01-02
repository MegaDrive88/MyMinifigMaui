using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiBeadando2.Classes {
    public class ApiService {
        private readonly HttpClient _httpClient = new();
        private readonly string _apiKey = Task.Run(()=>SecureStorage.GetAsync("API_KEY")).Result!;
        private readonly string url = "https://rebrickable.com/api/v3/lego/";
        public async Task<ObservableCollection<Part>> GetItemsAsync(PartCategoryEnum category, int page) {
            var response = await _httpClient.GetAsync($"{url}parts/?key={_apiKey}&part_cat_id={(int)category}&page_size=20&page={page}"); //pagek
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<PartsResponse>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return new ObservableCollection<Part>(data.Results ?? []);
        }

    //get /api/v3/lego/sets/{set_num}/parts/
    //get /api/v3/lego/minifigs/{set_num}/sets/
    //get /api/v3/lego/minifigs/{set_num}/parts/
    //get /api/v3/lego/parts/?part_cat_id
    //https://rebrickable.com/api/v3/swagger/?key=52ceebc4d805283cb30c451d693e2716#!/lego/lego_sets_parts_list
    //}
    }
    public class PartsResponse {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<Part> Results { get; set; }
    }
}
