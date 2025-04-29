using SavorySweets.Project.Models;
using SavorySweets.Project.Controllers;
using System.Collections.ObjectModel;

namespace SavorySweets.Project.Views
{
    public partial class MyRecipesPage : ContentPage
    {
        private readonly RecipeController _recipeController;  
        private readonly UserController _userController;      
        private ObservableCollection<Recipe> _myRecipes;       

        public MyRecipesPage()
        {
            InitializeComponent();
            _recipeController = new RecipeController();
            _userController = new UserController();
            LoadMyRecipes();
        }

        // Loads the logged-in user's recipes into the collection view
        private void LoadMyRecipes()
        {
            var user = _userController.GetLoggedInUser();
            if (user == null)
            {
                // If no user logged in, show error and navigate back
                DisplayAlert("Access Denied", "You must be logged in to view your recipes.", "OK");
                Navigation.PopAsync();
                return;
            }

            _myRecipes = _recipeController.GetUserRecipes(user.ID);
            myRecipeCollection.ItemsSource = _myRecipes;
        }

        // Navigates to the Add Recipe page
        private void OnAddRecipeClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddOrEditRecipePage(null, _recipeController, _userController));
        }

        // Navigates to the Edit Recipe page for the selected recipe
        private void OnEditRecipeClicked(object sender, EventArgs e)
        {
            if (sender is Button button && int.TryParse(button.CommandParameter.ToString(), out int id))
            {
                var recipe = _myRecipes.FirstOrDefault(r => r.Id == id);
                if (recipe != null)
                {
                    Navigation.PushAsync(new AddOrEditRecipePage(recipe, _recipeController, _userController));
                }
            }
        }

        // Deletes the selected recipe after user confirmation
        private async void OnDeleteRecipeClicked(object sender, EventArgs e)
        {
            if (sender is Button button && int.TryParse(button.CommandParameter.ToString(), out int id))
            {
                bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this recipe?", "Yes", "No");
                if (confirm)
                {
                    _recipeController.DeleteRecipe(id);
                    LoadMyRecipes(); // Reload the list after deletion
                }
            }
        }
    }
}
