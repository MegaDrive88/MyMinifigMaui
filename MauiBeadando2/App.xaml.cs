namespace MauiBeadando2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Application.Current!.UserAppTheme = AppTheme.Dark;
        }
        public async static Task CheckConnection() {
            while (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) {
                await Application.Current.MainPage.DisplayAlert(
                    "Nincs internet",
                    "Csatlakozzon az internethez!",
                    "OK");
            }
        }
    }
}
