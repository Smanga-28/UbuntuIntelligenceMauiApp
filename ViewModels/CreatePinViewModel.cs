using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace Ubuntu_Intelli_Compass.ViewModels
{
    public partial class CreatePinViewModel: ObservableObject
    {
        [ObservableProperty]
        private string  validationMessage = "";
        [ObservableProperty]
        private bool hasValidatedMessage;

        [ObservableProperty]
        private string pinDigit1 = "";

        [ObservableProperty]
        private string pinDigit2 = "";

        [ObservableProperty]
        private string pinDigit3 = "";
        
        [ObservableProperty]
        private string pinDigit4 = "";

        [ObservableProperty]
        private string pinDigit5 = "";

        [ObservableProperty]
        private string pinDigit6 = "";

        //Comfirm pin
        [ObservableProperty]
        private string confirmPinDigitEntry1 = "";

        [ObservableProperty]
        private string confirmPinDigitEntry2 = "";

        [ObservableProperty]
        private string confirmPinDigitEntry3 = "";

        [ObservableProperty]
        private string confirmPinDigitEntry4 = "";

        [ObservableProperty]
        private string confirmPinDigitEntry5 = "";

        [ObservableProperty]
        private string confirmPinDigitEntry6 = "";
        
        public string GetPin()

        {
            return PinDigit1 + PinDigit2 + PinDigit3 + PinDigit4 + PinDigit5 + PinDigit6;
        }
        
        public string ConfirmPin()
        {
            return ConfirmPinDigitEntry1 + ConfirmPinDigitEntry2 +
                ConfirmPinDigitEntry3 + ConfirmPinDigitEntry4 + ConfirmPinDigitEntry5 +
                ConfirmPinDigitEntry6;
        }
        [RelayCommand]
        public void ValidatePin()
        {
            string pin = GetPin();
            string confirmPin = ConfirmPin();

            if(pin.Length !=6 || confirmPin.Length != 6)
            {
                ValidationMessage = "please complete all Pin entries";
                HasValidatedMessage = true;
                return;
                 
            }

            if(pin != confirmPin)
            {
                ValidationMessage = "Pin not match";
                HasValidatedMessage= true;
                return;

            }
            ValidationMessage = "";
            HasValidatedMessage = false;
            
        }
    }
}
