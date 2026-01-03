using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExCSS;
using MauiBeadando2.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MauiBeadando2.ViewModels {
    public partial class PartDetailsViewModel : ObservableObject, IQueryAttributable {
        private ApiService apiService = new();

        [ObservableProperty]
        Part selectedPart;

        [ObservableProperty]
        Dictionary<string, string> urls;

        public void ApplyQueryAttributes(IDictionary<string, object> query) {
            SelectedPart = (Part)query["part"];
            Urls = new() {
                { "Rebrickable", SelectedPart.part_url }
            };
            foreach (var id in SelectedPart.external_ids) {
                string value = id.Key switch {
                    "BrickLink" => $"https://www.bricklink.com/v2/catalog/catalogitem.page?P={id.Value[0]}",
                    "BrickOwl" => $"https://www.brickowl.com/catalog/{id.Value[0]}",
                    "Brickset" => $"https://brickset.com/parts/design-{id.Value[0]}",
                    "LDraw" => $"https://library.ldraw.org/parts/search/suffix?basepart={id.Value[0]}",
                    "LEGO" => $"https://www.lego.com/en-us/pick-and-build/pick-a-brick?system=LEGO&selectedElement={SelectedPart.part_img_url.Split('/')[^1].Split('.')[0]}", // csak akkor mukodik ha in stock
                    _ => ""
                };
                if (value != "") Urls.Add(id.Key, value);
            }
        }
        #region TODO
        /* SQLite mentes torles stb
         * elemek torlese builder oldalon
         * Details oldal
         * bugfixek
         * mentes haromszog gombnal
         * feature - kedvencek - ha van ra ido
        */
        #endregion
        [RelayCommand]
        public async Task OpenUrl(string url) {
            await Launcher.OpenAsync(url);
        }
    }
}
