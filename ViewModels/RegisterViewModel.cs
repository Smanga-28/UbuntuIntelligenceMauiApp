
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SQLite;
using Ubuntu_Intelli_Compass.Models;
using Ubuntu_Intelli_Compass.Services;

namespace Ubuntu_Intelli_Compass.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly UbuntuDatabaseService _ubuntuDatabaseService;

       
        [ObservableProperty]
        private User newUser = new();

        // Displays registration feedback.
        [ObservableProperty]
        private string registrationMessage = string.Empty;

        private bool _isBusy;

        private int Age = 16;

        public RegisterViewModel(UbuntuDatabaseService ubuntuDatabaseService)
        {
            _ubuntuDatabaseService = ubuntuDatabaseService;
        }

        // Called when the user presses SIGN UP.
        [RelayCommand]
        private async Task RegisterAsync()
        {
            if (_isBusy)
                return;

            _isBusy = true;
            try
            {
                RegistrationMessage = string.Empty;

                if (await RegisterUserAsync(NewUser))
                {
                    RegistrationMessage = "Account created successfully!";
                    NewUser = new User();
                    await Shell.Current.GoToAsync("//CreatePinPage");

                }
            }
            finally
            {
                _isBusy = false;
            }
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            try
            {
                user.FirstName = (user.FirstName ?? "").Trim();
                user.LastName = (user.LastName ?? "").Trim();
                user.PhoneNumber = (user.PhoneNumber ?? "").Trim();
                user.Email = NormalizeEmail(user.Email);

                if (user.FirstName.Length == 0 ||
                    user.LastName.Length == 0 ||
                    user.Email.Length == 0 ||
                    user.PhoneNumber.Length == 0)
                {
                    RegistrationMessage = "Please complete all required fields.";
                    return false;
                }

                if (!ValidateEmail(user.Email))
                {
                    RegistrationMessage = "Please enter a valid email address.";
                    return false;
                }

                if (!Regex.IsMatch(user.PhoneNumber, @"^\+?[0-9\s\-]{9,10}$"))
                {
                    RegistrationMessage = "Please enter a valid phone number.";
                    return false;
                }

                
                if (user.DateOfBirth.Date >= DateTime.Today || user.DateOfBirth.Year < 1900)
                {
                    RegistrationMessage = "Please choose your date of birth.";
                    return false;
                }

                if (!IsUserAtleast16(user.DateOfBirth))
                {
                    RegistrationMessage = $"You must be at least {Age} years old to open an account.";
                    return false;
                }
                

                SQLiteAsyncConnection connection =
                    await _ubuntuDatabaseService.GetDbConnectionAsync();

                var existingUser = await connection.Table<User>()
                    .Where(u => u.Email == user.Email)
                    .FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    RegistrationMessage = "This email is already registered.";
                    return false;
                }

                user.CreatedAt = DateTime.UtcNow;

                int rows = await connection.InsertAsync(user);
                if (rows != 1)
                {
                    RegistrationMessage = "Your account could not be saved. Please try again.";
                    return false;
                }

                return true;
            }
            catch (SQLiteException ex) when (ex.Result == SQLite3.Result.Constraint)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                RegistrationMessage = "This email is already registered.";
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                RegistrationMessage = "Registration error: " + ex.Message;
                return false;
            }
        }

        private static string NormalizeEmail(string? email)
            => email?.Trim().ToLowerInvariant() ?? string.Empty;

        private bool IsUserAtleast16(DateTime dateOfBirth) 
        {
            DateTime minimumDateOfBirth = DateTime.Today.AddYears(-16);
            if(dateOfBirth.Date > minimumDateOfBirth)
            {
                return false;
            }
            return true;
        }

        private static bool ValidateEmail(string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
