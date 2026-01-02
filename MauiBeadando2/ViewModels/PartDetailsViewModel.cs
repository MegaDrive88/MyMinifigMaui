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
    public partial class PartDetailsViewModel : ObservableObject, IQueryAttributable {
        private ApiService apiService = new();

        public void ApplyQueryAttributes(IDictionary<string, object> query) {
            
        }
    }
}
