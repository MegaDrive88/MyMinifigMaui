using MauiBeadando2.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MauiBeadando2.Components;

public partial class MinifigPartRow : ContentView, INotifyPropertyChanged {
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
            PartCategoryEnum.HeadItem => "Fej kiegészítő",
            PartCategoryEnum.Head => "Fej (kötelező)",
            PartCategoryEnum.BackItem => "Nyak/hát kiegészítő",
            PartCategoryEnum.Torso => "Test (kötelező)",
            PartCategoryEnum.Leg => "Lábak (kötelező)",
            PartCategoryEnum.Accessory => "Fegyver/szerszám/kiegészítő",
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
            { "categoryString", CategoryString.Split("(")[0].Trim() }
        });
    }
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged(string tulajdonsagNev) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(tulajdonsagNev));
    }
}