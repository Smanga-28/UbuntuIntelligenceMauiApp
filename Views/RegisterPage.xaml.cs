using Ubuntu_Intelli_Compass.ViewModels;

namespace Ubuntu_Intelli_Compass.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterViewModel registerViewModel)
	{
		InitializeComponent();
        BindingContext = registerViewModel;
    }
}