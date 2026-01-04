using MauiBeadando2.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MauiBeadando2.Components;

public partial class MinifigPartRow : ContentView {
    public static BindableProperty MinifigPartProperty =
        BindableProperty.Create(
            nameof(MinifigPart), 
            typeof(Part), 
            typeof(MinifigPartRow),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnMinifigPartChanged);

    private static void OnMinifigPartChanged(BindableObject bindable, object oldValue, object newValue) {
        var control = (MinifigPartRow)bindable;
        control.DetailsIsEnabled = newValue != null;
        control.OnPropertyChanged(nameof(MinifigPart));
    }

    public Part? MinifigPart {
        get => (Part?)GetValue(MinifigPartProperty);
        set => SetValue(MinifigPartProperty, value);
    }

    public static readonly BindableProperty CategoryProperty =
        BindableProperty.Create(
            nameof(Category), 
            typeof(PartCategoryEnum), 
            typeof(MinifigPartRow),
            propertyChanged: OnCategoryChanged);

    private static void OnCategoryChanged(BindableObject bindable, object oldValue, object newValue) {
        var control = (MinifigPartRow)bindable;
        control.CategoryString = (PartCategoryEnum)newValue switch {
            PartCategoryEnum.HeadItem => "Fejviselet",
            PartCategoryEnum.Head => "Fej",
            PartCategoryEnum.BackItem => "Nyak/hát kiegészítő",
            PartCategoryEnum.Torso => "Test",
            PartCategoryEnum.Leg => "Lábak",
            PartCategoryEnum.Accessory => "Fegyver/szerszám/kiegészítő",
            PartCategoryEnum.HeadwearAccessory => "Fejviselet kiegészítő",
            PartCategoryEnum.Hipwear => "Csípő kiegészítő",
            _ => string.Empty
        };
    }

    public PartCategoryEnum Category {
        get => (PartCategoryEnum)GetValue(CategoryProperty);
        set => SetValue(CategoryProperty, value);
    } 

    private string categoryString;
    public string CategoryString {
        get {
            return categoryString;
        }
        set {
            categoryString = value;
            OnPropertyChanged(nameof(CategoryString));
        }
    }

    private bool detailsIsEnabled;
    public bool DetailsIsEnabled {
        get {
            return detailsIsEnabled;
        }
        set {
            detailsIsEnabled = value;
            OnPropertyChanged(nameof(DetailsIsEnabled));
        }
    }

    public MinifigPartRow()
	{
		InitializeComponent();
        BindingContext = this;
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        await Shell.Current.GoToAsync("PartSelectorPage", new Dictionary<string, object> {
            { "category", Category },
            { "categoryString", CategoryString }
        });
    }
    private async void DetailsBtnClick(object sender, EventArgs e) {
        await Shell.Current.GoToAsync("PartDetailsPage", new Dictionary<string, object> {
            { "part", MinifigPart },
        });
    }

    private void DeleteClicked(object sender, EventArgs e) {
        MinifigPart = null;
    }
}