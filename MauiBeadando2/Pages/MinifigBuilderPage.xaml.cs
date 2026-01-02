using MauiBeadando2.ViewModels;

namespace MauiBeadando2.Pages;

public partial class MinifigBuilderPage : ContentPage
{
	public MinifigBuilderPage(MinifigBuilderViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior {
            IsVisible = false
        });
    }
}