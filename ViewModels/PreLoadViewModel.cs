using System.ComponentModel;
using Ubuntu_Intelli_Compass.Services;

namespace Ubuntu_Intelli_Compass.ViewModels;

public class PreLoadViewModel : INotifyPropertyChanged
{
    private readonly UbuntuDatabaseService _databaseService;

    public event PropertyChangedEventHandler? PropertyChanged;

    public PreLoadViewModel(UbuntuDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    private double progress;
    public double Progress
    {
        get => progress;
        set { progress = value; OnPropertyChanged(nameof(Progress)); }
    }

    private string titleText = "Loading...";
    public string TitleText
    {
        get => titleText;
        set { titleText = value; OnPropertyChanged(nameof(TitleText)); }
    }

    private string statusText = "Please wait...";
    public string StatusText
    {
        get => statusText;
        set { statusText = value; OnPropertyChanged(nameof(StatusText)); }
    }

    public async Task StartLoading()
    {
        StatusText = "Loading settings...";
        await Task.Delay(800);
        Progress = 0.25;

        // Open (and if needed copy) the database here so that any problem shows
        // on this screen instead of the first time someone taps SIGN UP.
        StatusText = "Preparing data...";
        try
        {
            await _databaseService.GetDbConnectionAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            TitleText = "Something went wrong";
            StatusText = "Could not open the database: " + ex.Message;
            return;
        }
        Progress = 0.50;

        StatusText = "Checking permissions...";
        await Task.Delay(800);
        Progress = 0.75;

        StatusText = "Almost there...";
        await Task.Delay(800);
        Progress = 1.0;

        TitleText = "Ready!";
        await Task.Delay(500);

        await Shell.Current.GoToAsync("//RegisterPage");
    }

    private void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
