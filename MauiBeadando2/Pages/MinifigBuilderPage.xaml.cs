using MauiBeadando2.ViewModels;

namespace MauiBeadando2.Pages;

public partial class MinifigBuilderPage : ContentPage
{
    MinifigBuilderViewModel _viewModel;
    public MinifigBuilderPage(MinifigBuilderViewModel vm)
	{
		InitializeComponent();
        App.CheckConnection();
        BindingContext = vm;
        _viewModel = vm;
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior {
            IsVisible = false
        });
    }
    protected override bool OnBackButtonPressed() {
        _viewModel.SaveButtonCommand.Execute(null);
        return true;
    }
}