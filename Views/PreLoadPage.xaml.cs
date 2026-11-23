using Ubuntu_Intelli_Compass.ViewModels;

namespace Ubuntu_Intelli_Compass.Views;

public partial class PreLoadPage : ContentPage
{
	private readonly PreLoadViewModel _viewModel;
	
    public PreLoadPage(PreLoadViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }
	private bool _started;

    protected override void OnAppearing()
    {
        
        base.OnAppearing();
        if(_started)
            return;

        _started = true;
        
    }
}