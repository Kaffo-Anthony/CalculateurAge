namespace CalculateurAge.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
    private string _nom = "";
    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);
    private string _resultat = "";
    private bool _resultatVisible;
    private string _joursRestants = "";

    public string Nom
    {
        get => _nom;
        set { if (SetField(ref _nom, value)) CalculerCommand.Rafraichir(); }
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

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set => SetField(ref _resultatVisible, value);
    }
    public string JoursRestants
    {
    get => _joursRestants;
    set => SetField(ref _joursRestants, value);
    }

    public RelayCommand CalculerCommand { get; }
    public RelayCommand EffacerCommand { get; } 

    public CalculateurViewModel()
    {
        CalculerCommand = new RelayCommand(Calculer, () => !string.IsNullOrWhiteSpace(Nom));
        EffacerCommand = new RelayCommand(Effacer);
    }

    private void Calculer()
    {
        int age = DateTime.Today.Year - DateNaissance.Year;
        if (DateNaissance.Date > DateTime.Today.AddYears(-age)) age--;
        Resultat = $"{Nom}, vous avez {age} ans";
        Message = age >= 18 ? "Majeur" : "Mineur";
        DateTime aujourdhui = DateTime.Today;
        DateTime prochain = Anniversaire(aujourdhui.Year, DateNaissance);
        if (prochain < aujourdhui)
            prochain = Anniversaire(aujourdhui.Year + 1, DateNaissance);
        int jours = (prochain - aujourdhui).Days;
        JoursRestants = jours == 0
        ? "Joyeux anniversaire !"
    :   $"Prochain anniversaire dans {jours} jour(s)";
        ResultatVisible = true;
    }
    private void Effacer()
   {
    Nom = "";
    DateNaissance = DateTime.Today.AddYears(-20);
    Resultat = "";
    Message = "";            
    ResultatVisible = false;
    JoursRestants = "";
    }
    private string _message = "";
    public string Message
{
    get => _message;
    set => SetField(ref _message, value);
}
private static DateTime Anniversaire(int annee, DateTime naissance)
{
    int jour = naissance.Day;
    if (naissance.Month == 2 && jour == 29 && !DateTime.IsLeapYear(annee))
        jour = 28;
    return new DateTime(annee, naissance.Month, jour);
}
}