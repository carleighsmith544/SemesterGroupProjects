using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavorySweets.Project.Controllers;

namespace SavorySweets.Project.Views
{
    public partial class PopularRecipesPage : ContentPage
    {
        private readonly RecipeController _recipeController; 

        public PopularRecipesPage()
        {
            InitializeComponent();
            _recipeController = new RecipeController();
            LoadPopularRecipes();
        }

        // Loads recipes that are categorized as "Popular"
        private void LoadPopularRecipes()
        {
            var popular = _recipeController.Recipes
                .Where(r => r.Category != null && r.Category.ToLower().Contains("popular")) 
                .Select(r => new RecipeDisplayView(r)) 
                .ToList();

            // Bind the popular recipes to the CollectionView
            popularRecipesCollection.ItemsSource = new ObservableCollection<RecipeDisplayView>(popular);
        }

        // Handles when the favorite icon is clicked to toggle favorite status
        private void OnToggleFavoriteClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.CommandParameter is int id)
            {
                _recipeController.ToggleFavorite(id);
                LoadPopularRecipes(); 
            }
        }

        // Handles when a popular recipe is selected from the CollectionView
        private void OnRecipeSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is RecipeDisplayView selected)
            {
                
                var recipe = _recipeController.Recipes.FirstOrDefault(r => r.Id == selected.Id);
                if (recipe != null)
                {
                    Navigation.PushAsync(new RecipeDetailPage(recipe, _recipeController));
                }

                popularRecipesCollection.SelectedItem = null;
            }
        }
    }
}
