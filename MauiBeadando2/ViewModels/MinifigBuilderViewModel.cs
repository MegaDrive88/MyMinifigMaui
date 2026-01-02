using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiBeadando2.Classes;
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
                        CurrentMinifig.HeadItemId = selectedPart.part_num;
                        break;
                    case PartCategoryEnum.Head:
                        CurrentMinifig.HeadPart = selectedPart;
                        CurrentMinifig.HeadPartId = selectedPart.part_num;
                        break;
                    case PartCategoryEnum.BackItem:
                        CurrentMinifig.BackItem = selectedPart;
                        CurrentMinifig.BackItemId = selectedPart.part_num;
                        break;
                    case PartCategoryEnum.Torso:
                        CurrentMinifig.TorsoPart = selectedPart;
                        CurrentMinifig.TorsoPartId = selectedPart.part_num;
                        break;
                    case PartCategoryEnum.Leg:
                        CurrentMinifig.LegPart = selectedPart;
                        CurrentMinifig.LegPartId = selectedPart.part_num;
                        break;
                    case PartCategoryEnum.Accessory:
                        CurrentMinifig.Accessory = selectedPart;
                        CurrentMinifig.AccessoryId = selectedPart.part_num;
                        break;
                }
                var temp = CurrentMinifig;
                CurrentMinifig = null;
                CurrentMinifig = temp;
            }
        }
        
        [RelayCommand]
        public async void BackToMainPage() {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
