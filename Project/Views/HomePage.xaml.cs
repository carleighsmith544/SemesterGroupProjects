using SavorySweets.Project.Controllers;
using SavorySweets.Project.Models;

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

        // Navigates to the full recipe list
        private void OnViewRecipesClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecipeListPage());
        }

        // Navigates to the user's list of favorite recipes
        private void OnViewFavoritesClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FavoritesPage());
        }

        // Navigates to the page showing the current user's own recipes
        private void OnMyRecipesClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyRecipesPage());
        }

        // Navigates to the app settings page
        private void OnSettingsClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }

        // Navigates to the Sign In page
        private void OnSignInClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignInPage());
        }

        // Navigates to the Sign Up page
        private void OnSignUpClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }

        // Navigates to a list of popular recipes
        private void OnPopularClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PopularRecipesPage());
        }

        // Navigates to gluten-free recipes
        private void OnGlutenFreeClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GlutenFreeRecipesPage());
        }

        // Navigates to recipes with a prep time of 5 minutes or less
        private void OnFiveMinuteClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FiveMinuteRecipesPage());
        }

        // Navigates to beginner-friendly recipes
        private void OnBeginnerClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BeginnerRecipesPage());
        }

        // Logs out the current user and updates UI
        private void OnLogoutClicked(object sender, EventArgs e)
        {
            _userController.Logout();
            _currentUser = null;
            UpdateUIForUserState();
        }

        // Updates visibility of sign-in/sign-up/logout buttons based on user login status
        private void UpdateUIForUserState()
        {
            bool isLoggedIn = _currentUser != null;

            SignInButton.IsVisible = !isLoggedIn;
            SignUpButton.IsVisible = !isLoggedIn;
            LogoutButton.IsVisible = isLoggedIn;

        }
    }
}
