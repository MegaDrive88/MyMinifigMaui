using MauiBeadando2.ViewModels;

namespace MauiBeadando2.Pages;

public partial class PartDetailsPage : ContentPage {
    public PartDetailsPage(PartDetailsViewModel vm) {
        InitializeComponent();
        BindingContext = vm;
        Shell.SetForegroundColor(this, Colors.Yellow);
    }
}