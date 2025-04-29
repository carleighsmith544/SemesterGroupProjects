using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private void LoadMyRecipes()
        {
            var user = _userController.GetLoggedInUser();
            if (user == null)
            {
                DisplayAlert("Access Denied", "You must be logged in to view your recipes.", "OK");
                Navigation.PopAsync();
                return;
            }

            _myRecipes = _recipeController.GetUserRecipes(user.ID);
            myRecipeCollection.ItemsSource = _myRecipes;
        }

        private void OnAddRecipeClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddOrEditRecipePage(null, _recipeController, _userController));
        }

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

        private async void OnDeleteRecipeClicked(object sender, EventArgs e)
        {
            if (sender is Button button && int.TryParse(button.CommandParameter.ToString(), out int id))
            {
                bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this recipe?", "Yes", "No");
                if (confirm)
                {
                    _recipeController.DeleteRecipe(id);
                    LoadMyRecipes(); // Refresh
                }
            }
        }
    }
}

