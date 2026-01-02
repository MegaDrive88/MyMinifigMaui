using MauiBeadando2.Pages;

namespace MauiBeadando2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute("MinifigBuilderPage", typeof(MinifigBuilderPage));
            Routing.RegisterRoute("MinifigBuilderPage/PartSelectorPage", typeof(PartSelectorPage));
            InitializeComponent();
        }
    }
}
