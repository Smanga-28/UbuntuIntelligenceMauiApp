using Ubuntu_Intelli_Compass.ViewModels;

namespace Ubuntu_Intelli_Compass.Views;

public partial class PreLoadPage : ContentPage
{
    private readonly PreLoadViewModel _viewModel;
    private bool _started;

    public PreLoadPage(PreLoadViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_started)
            return;
        _started = true;

        try
        {
            await _viewModel.StartLoading();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            _viewModel.TitleText = "Something went wrong";
            _viewModel.StatusText = ex.Message;
        }
    }
}
