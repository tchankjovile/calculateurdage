using CalculateurAGE.ViewModels;

namespace CalculateurAGE;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        // Objet dans lequel tous les {Binding} vont chercher leurs valeurs.
        BindingContext = new CalculateurViewModel();
    }
}