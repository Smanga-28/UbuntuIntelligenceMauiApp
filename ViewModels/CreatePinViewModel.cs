using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using Ubuntu_Intelli_Compass.Services;

namespace Ubuntu_Intelli_Compass.ViewModels
{
    public partial class CreatePinViewModel : ObservableObject
    {
        private readonly UbuntuDatabaseService _ubuntuDatabaseService;

        [ObservableProperty]


        public CreatePinViewModel(UbuntuDatabaseService ubuntuDatabaseService)
        {
            _ubuntuDatabaseService = ubuntuDatabaseService;
        }

        public int CreatePin()
        {

        }
    }
}
