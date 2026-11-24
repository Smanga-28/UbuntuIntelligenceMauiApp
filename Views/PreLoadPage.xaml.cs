using Ubuntu_Intelli_Compass.ViewModels;

namespace Ubuntu_Intelli_Compass.Views;

public partial class PreLoadPage : ContentPage
{
    private bool isLoading;

    public PreLoadPage()
    {
        InitializeComponent();
        BindingContext = new PreLoadViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (isLoading)
            return;

        if (BindingContext is not PreLoadViewModel vm)
            return;

        isLoading = true;
        try
        {
            await vm.StartLoading();
        }
        finally
        {
            isLoading = false;
        }
    }
}