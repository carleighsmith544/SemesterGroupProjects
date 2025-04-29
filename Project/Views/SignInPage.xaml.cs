using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavorySweets.Project.Controllers;

namespace SavorySweets.Project.Views
{
    public partial class SignInPage : ContentPage
    {
        private readonly UserController _userController;

        public SignInPage()
        {
            InitializeComponent();
            _userController = new UserController();
        }
        private void OnSignInClicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text?.Trim() ?? "";
            string password = passwordEntry.Text ?? "";

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                errorLabel.Text = "Please enter both email and password.";
                errorLabel.IsVisible = true;
                return;
            }

            bool success = _userController.Login(email, password);
            if (success)
            {
                errorLabel.IsVisible = false;

                // Navigate to Main Page or Home Page
                Application.Current.MainPage = new NavigationPage(new HomePage()); // Replace with your actual home page
            }
            else
            {
                errorLabel.Text = "Login failed. Please check your credentials.";
                errorLabel.IsVisible = true;
            }
        }

        private void OnSignUpClicked(object sender, EventArgs e)
        {
            // Navigate to Sign Up page
            Navigation.PushAsync(new SignUpPage());
        }
    }
}

