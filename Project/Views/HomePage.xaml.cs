using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavorySweets.Project.Controllers;
using SavorySweets.Project.Models;
using Microsoft.Maui.Storage;



namespace SavorySweets.Project.Views
{
    public partial class HomePage : ContentPage
    {

        private readonly UserController _userController;
        private User? _currentUser; 
        public HomePage() 
        {
            InitializeComponent();
            _userController = new UserController();
            _currentUser = _userController.GetLoggedInUser();
            UpdateUIForUserState();

        }

        private void OnViewRecipesClicked(object sender, EventArgs e)
        {
            // Replace with actual RecipesPage when it's built
            Navigation.PushAsync(new RecipeListPage());
        }

        private void OnViewFavoritesClicked(object sender, EventArgs e)
        {
            // Replace with actual FavoritesPage
            Navigation.PushAsync(new FavoritesPage());
        }

        private void OnMyRecipesClicked(object sender, EventArgs e)
        {
            // Replace with actual MyRecipesPage
            Navigation.PushAsync(new MyRecipesPage());
        }

        private void OnSettingsClicked(object sender, EventArgs e)
        {
            // Replace with actual SettingsPage
            Navigation.PushAsync(new SettingsPage());
        }

        private void OnSignInClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignInPage());
        }

        private void OnSignUpClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }
        private void OnPopularClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PopularRecipesPage());
        }

        private void OnGlutenFreeClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GlutenFreeRecipesPage());
        }

        private void OnFiveMinuteClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FiveMinuteRecipesPage());
        }

        private void OnBeginnerClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BeginnerRecipesPage());
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            _userController.Logout();
            _currentUser = null;
            UpdateUIForUserState();
        }


        private void UpdateUIForUserState()
        {
            bool isLoggedIn = _currentUser != null;

            SignInButton.IsVisible = !isLoggedIn;
            SignUpButton.IsVisible = !isLoggedIn;
            LogoutButton.IsVisible = isLoggedIn;

            // Optionally always visible: SettingsButton, MyRecipesButton
        }

    }
}

