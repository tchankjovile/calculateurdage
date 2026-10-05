namespace CalculateurAGE.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
    private string _nom = "";
    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);
    private string _resultat = "";
    private string _message = "";
    private string _joursAnniversaire = "";
    private bool _resultatVisible;

    public string Nom
    {
        get => _nom;
        set
        {
            if (SetField(ref _nom, value))
                CalculerCommand.Rafraichir();
        }
    }

    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set => SetField(ref _dateNaissance, value);
    }

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    // Fonctionnalité 1 : Majeur / Mineur
    public string Message
    {
        get => _message;
        set => SetField(ref _message, value);
    }

    // Fonctionnalité 3 : jours avant le prochain anniversaire
    public string JoursAnniversaire
    {
        get => _joursAnniversaire;
        set => SetField(ref _joursAnniversaire, value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set => SetField(ref _resultatVisible, value);
    }

    public RelayCommand CalculerCommand { get; }

    // Fonctionnalité 2 : Effacer
    public RelayCommand EffacerCommand { get; }

    public CalculateurViewModel()
    {
        CalculerCommand = new RelayCommand(
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom));

        EffacerCommand = new RelayCommand(Effacer);
    }

    private void Calculer()
    {
        int age = DateTime.Today.Year - DateNaissance.Year;
        if (DateNaissance.Date > DateTime.Today.AddYears(-age)) age--;

        Resultat = $"{Nom}, vous avez {age} ans";
        Message = age >= 18 ? "Statut : Majeur" : "Statut : Mineur";

        int jours = JoursAvantAnniversaire();
        JoursAnniversaire = jours == 0
            ? "Joyeux anniversaire aujourd'hui !"
            : $"Prochain anniversaire dans {jours} jour(s)";

        ResultatVisible = true;
    }

    private int JoursAvantAnniversaire()
    {
        DateTime today = DateTime.Today;
        DateTime prochain = AnniversairePourAnnee(today.Year);
        if (prochain < today)
            prochain = AnniversairePourAnnee(today.Year + 1);
        return (prochain - today).Days;
    }

    // Gère le 29 février les années non bissextiles.
    private DateTime AnniversairePourAnnee(int annee)
    {
        int jour = DateNaissance.Day;
        if (DateNaissance.Month == 2 && jour == 29
            && !DateTime.IsLeapYear(annee))
            jour = 28;
        return new DateTime(annee, DateNaissance.Month, jour);
    }

    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = "";
        Message = "";
        JoursAnniversaire = "";
        ResultatVisible = false;
    }
}