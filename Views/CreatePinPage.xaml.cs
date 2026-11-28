namespace Ubuntu_Intelli_Compass.Views;

public partial class CreatePinPage : ContentPage
{
	public CreatePinPage()
	{
		InitializeComponent();
        BindingContext = new CreatePinPage();
	}

    private void OnPinTextChanged(object sender, TextChangedEventArgs e)
    {
        Entry currentEntry = (Entry)sender;
        if (!string.IsNullOrEmpty(e.NewTextValue))

        {
            if (currentEntry == PinDigitEntry1)
            {
                PinDigitEntry2.Focus();
            }
            else if (currentEntry == PinDigitEntry2)
            {
                PinDigitEntry3.Focus();
            }
            else if (currentEntry == PinDigitEntry3)
            {
                PinDigitEntry4.Focus();
            }
            else if (currentEntry == PinDigitEntry4)
            {

                PinDigitEntry5.Focus();
            }
            else if (currentEntry == PinDigitEntry5)
            {
                PinDigitEntry6.Focus();
            }

            // Confirm Pin
            else if (currentEntry == ConfirmPinDigitEntry1)
            {
                ConfirmPinDigitEntry2.Focus();
            } 
            else if (currentEntry == ConfirmPinDigitEntry2)
            {
                ConfirmPinDigitEntry3.Focus();
            }
            else if (currentEntry == ConfirmPinDigitEntry3)
            {
                ConfirmPinDigitEntry4.Focus();
            }
            else if (currentEntry == ConfirmPinDigitEntry4)
            {
                ConfirmPinDigitEntry5.Focus();
            }
            else if (currentEntry == ConfirmPinDigitEntry5)
            {
                ConfirmPinDigitEntry6.Focus();
            }
        }
    }
    
}