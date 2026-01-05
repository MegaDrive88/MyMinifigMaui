using MauiBeadando2.ViewModels;

namespace MauiBeadando2
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel vm) {
            InitializeComponent();
            App.CheckConnection();
            BindingContext = vm;
        }
    }
}
