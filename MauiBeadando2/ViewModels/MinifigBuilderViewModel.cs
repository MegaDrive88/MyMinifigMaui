using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiBeadando2.Classes;
using MauiBeadando2.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBeadando2.ViewModels {
    public partial class MinifigBuilderViewModel : ObservableObject, IQueryAttributable {
        [ObservableProperty]
        Minifig? currentMinifig;

        private MinifigDatabase Database { get; set; } = new();


        public void ApplyQueryAttributes(IDictionary<string, object> query) {
            if (query.ContainsKey("minifig")) {
                CurrentMinifig = (Minifig)query["minifig"];
            }
            if (query.ContainsKey("selectedPart") && CurrentMinifig != null) {
                var selectedPart = (Part)query["selectedPart"];
                var category = (PartCategoryEnum)query["partCategory"];

                switch (category) {
                    case PartCategoryEnum.HeadItem:
                        CurrentMinifig.HeadItem = selectedPart;
                        break;
                    case PartCategoryEnum.Head:
                        CurrentMinifig.HeadPart = selectedPart;
                        break;
                    case PartCategoryEnum.BackItem:
                        CurrentMinifig.BackItem = selectedPart;
                        break;
                    case PartCategoryEnum.Torso:
                        CurrentMinifig.TorsoPart = selectedPart;
                        break;
                    case PartCategoryEnum.Leg:
                        CurrentMinifig.LegPart = selectedPart;
                        break;
                    case PartCategoryEnum.Accessory:
                        CurrentMinifig.Accessory = selectedPart;
                        break;
                    case PartCategoryEnum.HeadwearAccessory:
                        CurrentMinifig.HeadwearAccessory = selectedPart;
                        break;
                    case PartCategoryEnum.Hipwear:
                        CurrentMinifig.Hipwear = selectedPart;
                        break;
                }
                var temp = CurrentMinifig;
                CurrentMinifig = null;
                CurrentMinifig = temp;
            }
        }

        [RelayCommand]
        public async Task BackToMainPage() {
            await Shell.Current.GoToAsync("//MainPage");
        }
        [RelayCommand]
        public async Task SaveButton() {
            try {
                await Database.SaveMinifigAsync(CurrentMinifig);
                CurrentMinifig.IsDeleteReady = true;
                await Shell.Current.GoToAsync("//MainPage");
            }
            catch (SQLite.SQLiteException e) {
                if (e.Message.Contains("UNIQUE")) await Application.Current.MainPage.DisplayAlert("Hiba", $"A figura neve egyedi kell legyen!", "Ok");
                else await Application.Current.MainPage.DisplayAlert("Hiba", $"Nem sikerült menteni: {e.Message}", "Ok");
            }

        }
    }
}
