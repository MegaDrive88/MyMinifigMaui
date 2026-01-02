using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiBeadando2.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBeadando2.ViewModels {
    public partial class PartSelectorViewModel : ObservableObject, IQueryAttributable {
        private ApiService apiService = new();

        [ObservableProperty]
        ObservableCollection<Part> parts;

        PartCategoryEnum Category;

        [ObservableProperty]
        string? categoryString;
        [ObservableProperty]
        int pageNum = 1;

        public void ApplyQueryAttributes(IDictionary<string, object> query) {
            Category = (PartCategoryEnum)query["category"];
            CategoryString = (string)query["categoryString"];
            PageNum = 1;
            Parts = Task.Run(() => apiService.GetItemsAsync(Category, PageNum)).Result;
            // Java.Lang.RuntimeException: 'Canvas: trying to use a recycled bitmap android.graphics.Bitmap@bb671a8'
            //searchbar
        }

        [RelayCommand]
        public async void PartTapped(object part) {
            await Shell.Current.GoToAsync("MinifigBuilderPage", new Dictionary<string, object> {
                { "selectedPart", part },
                { "partCategory", Category }
            });
        }
        [RelayCommand]
        public void TurnPage(string way) {
            int oldPageNum = PageNum;
            if (way == "left") PageNum--;
            else PageNum++;
            try {
                Parts = Task.Run(() => apiService.GetItemsAsync(Category, PageNum)).Result;
            }
            catch {
                PageNum = oldPageNum;
            }
        }
        [RelayCommand]
        public async Task BackButton() {
            await Shell.Current.GoToAsync("..");
        }
    }
}
