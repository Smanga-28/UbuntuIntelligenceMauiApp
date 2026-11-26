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

        // Holds the information entered by the user.
        [ObservableProperty]
        public User newUser = new();

        // Displays registration feedback.
        [ObservableProperty]
        private string registrationMessage = string.Empty;

        public RegisterViewModel(UbuntuDatabaseService ubuntuDatabaseService)
        {
            _ubuntuDatabaseService = ubuntuDatabaseService;
        }

        // Called when the user presses Sign Up.
        [RelayCommand]
        private async Task RegisterAsync()
        {
            RegistrationMessage = string.Empty;

            bool isRegistered = await RegisterUserAsync(NewUser);

            if (isRegistered)
            {
                RegistrationMessage = "Account created successfully!";

                NewUser = new User();
            }
        }

        // Handles the actual registration process.
        public async Task<bool> RegisterUserAsync(User user)
        {
            try
            {
                // Check required information.
                if (string.IsNullOrWhiteSpace(user.FirstName) ||
                    string.IsNullOrWhiteSpace(user.LastName) ||
                    string.IsNullOrWhiteSpace(user.Email) ||
                    string.IsNullOrWhiteSpace(user.PhoneNumber))
                {
                    RegistrationMessage = "Please complete all required fields.";
                    return false;
                }

                // Normalise email.
                user.Email = NormalizeEmail(user.Email);

                // Validate email.
                if (!ValidateEmail(user.Email))
                {
                    RegistrationMessage = "Please enter a valid email address.";
                    return false;
                }

                // Get database connection.
                SQLiteAsyncConnection connection =
                    await _ubuntuDatabaseService.GetDbConnectionAsync();

                // Check if email already exists.
                var existingUser = await connection.Table<User>()
                    .Where(u => u.Email == user.Email)
                    .FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    RegistrationMessage = "This email is already registered.";
                    return false;
                }

                // Save the new user.
                await connection.InsertAsync(user);

                return true;
            }
            catch (Exception ex)
            {
                RegistrationMessage = "Registration error: " + ex.Message;

                Console.WriteLine(ex.ToString());

                return false;
            }
        }

        // Remove unnecessary spaces and convert email to lowercase.
        private static string NormalizeEmail(string email)
        {
            return email?.Trim().ToLowerInvariant() ?? string.Empty;
        }

        // Validate email format.
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
