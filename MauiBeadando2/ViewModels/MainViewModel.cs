using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiBeadando2.Classes;
using MauiBeadando2.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MauiBeadando2.ViewModels {
    public partial class MainViewModel : ObservableObject {
        [ObservableProperty]
        ObservableCollection<Minifig> minifigs;
        [ObservableProperty]
        Minifig? currentMinifig;

        public MinifigDatabase Database { get; set; } = new();

        [RelayCommand]
        private async void MinifigFrameTap(object minifig) {
            CurrentMinifig = (Minifig)minifig;

            await Shell.Current.GoToAsync("MinifigBuilderPage", new Dictionary<string, object>
            {
                { "minifig", CurrentMinifig }
            });
        }
        [RelayCommand]
        public void Appearing() {
            Minifigs = Task.Run(Database.GetAllItemsAsync).Result;
            Minifigs.Add(new());
        }
    }
}
