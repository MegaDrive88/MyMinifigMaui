using MauiBeadando2.ViewModels;

namespace MauiBeadando2.Pages;

public partial class PartSelectorPage : ContentPage
{
	public PartSelectorPage(PartSelectorViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior {
            IsVisible = false
        });
    }
}