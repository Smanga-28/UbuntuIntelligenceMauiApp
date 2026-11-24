using System.ComponentModel;

namespace Ubuntu_Intelli_Compass.ViewModels;

public class PreLoadViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private double progress;
    public double Progress
    {
        get { return progress; }
        set
        {
            progress = value;
            OnPropertyChanged("Progress");
        }
    }

    
    private string titleText = "Loading...";
    public string TitleText
    {
        get { return titleText; }
        set
        {
            titleText = value;
            OnPropertyChanged("TitleText");
        }
    }

    
    private string statusText = "Please wait...";
    public string StatusText
    {
        get { return statusText; }
        set
        {
            statusText = value;
            OnPropertyChanged("StatusText");
        }
    }

   
    public async Task StartLoading()
    {
        StatusText = "Loading settings...";
        await Task.Delay(1000);  
        Progress = 0.25;

        StatusText = "Preparing data...";
        await Task.Delay(1000);
        Progress = 0.50;

        StatusText = "Checking permissions...";
        await Task.Delay(1000);
        Progress = 0.75;

        StatusText = "Almost there...";
        await Task.Delay(1000);
        Progress = 1.0;

        TitleText = "Ready!";
        await Task.Delay(500);

       
        if (Shell.Current is not null)
            await Shell.Current.GoToAsync("//RegisterPage");
    }

    
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}