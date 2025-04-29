using SavorySweets.Project.Models;
using SavorySweets.Project.Controllers;

namespace SavorySweets.Project.Views
{
    public partial class SignUpPage : ContentPage
    {
        private readonly UserController _userController; 

        public SignUpPage()
        {
            InitializeComponent();
            _userController = new UserController();
        }

        // Handles the Create Account button click
        private void OnCreateAccountClicked(object sender, EventArgs e)
        {
            string firstName = firstNameEntry.Text?.Trim() ?? "";
            string lastName = lastNameEntry.Text?.Trim() ?? "";
            string email = emailEntry.Text?.Trim() ?? "";
            string password = passwordEntry.Text ?? "";

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                errorLabel.Text = "Please fill in all fields.";
                errorLabel.IsVisible = true;
                return;
            }

            // Create a new User object with provided input
            var newUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            bool success = _userController.Register(newUser);
            if (success)
            {
                errorLabel.IsVisible = false;
                DisplayAlert("Success", "Account created!", "OK");
                Navigation.PopAsync(); // Navigate back to Sign In page
            }
            else
            {
                errorLabel.Text = "An account with this email already exists.";
                errorLabel.IsVisible = true;
            }
        }

        // Handles the "Already have an account?" link/button
        private void OnSignInClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(); 
        }
    }
}
