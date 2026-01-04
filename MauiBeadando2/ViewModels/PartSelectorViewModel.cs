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
        [ObservableProperty]
        ObservableCollection<Part> parts;

        PartCategoryEnum Category;

        [ObservableProperty]
        string? categoryString;
        [ObservableProperty]
        int pageNum = 1;
        [ObservableProperty]
        string searchbarValue = string.Empty;

        public void ApplyQueryAttributes(IDictionary<string, object> query) {
            Category = (PartCategoryEnum)query["category"];
            CategoryString = (string)query["categoryString"];
            PageNum = 1;
            SearchbarValue = "";
            Parts = Task.Run(() => ApiService<Part>.GetItemsAsync(Category, PageNum)).Result;
        }

        [RelayCommand]
        public async Task PartTapped(object part) {
            await Shell.Current.GoToAsync("..", new Dictionary<string, object> {
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
                Parts = Task.Run(() => ApiService<Part>.GetItemsAsync(Category, PageNum, SearchbarValue)).Result;
            }
            catch {
                PageNum = oldPageNum;
            }
        }
        [RelayCommand]
        public async void BackButton() {
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        public void SearchbarTyping() {
            PageNum = 1;
            Parts = Task.Run(() => ApiService<Part>.GetItemsAsync(Category, PageNum, SearchbarValue)).Result;
        }
    }
}
