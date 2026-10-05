using CalculateurAGE.Views;

namespace CalculateurAGE;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(ResultatPage), typeof(ResultatPage));
    }
}