using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavorySweets.Project.Controllers;

namespace SavorySweets.Project.Views
{
    public partial class GlutenFreeRecipesPage : ContentPage
    {
        private readonly RecipeController _recipeController;

        public GlutenFreeRecipesPage()
        {
            InitializeComponent();
            _recipeController = new RecipeController();
            LoadGlutenFreeRecipes();
        }
        private void LoadGlutenFreeRecipes()
        {
            var glutenFree = _recipeController.Recipes
                .Where(r => r.Category != null && r.Category.ToLower().Contains("gluten-free"))
                .Select(r => new RecipeDisplayView(r))
                .ToList();

            glutenFreeCollection.ItemsSource = new ObservableCollection<RecipeDisplayView>(glutenFree);
        }

        private void OnToggleFavoriteClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.CommandParameter is int id)
            {
                _recipeController.ToggleFavorite(id);
                LoadGlutenFreeRecipes(); // Refresh
            }
        }

        private void OnRecipeSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is RecipeDisplayView selected)
            {
                var recipe = _recipeController.Recipes.FirstOrDefault(r => r.Id == selected.Id);
                if (recipe != null)
                {
                    Navigation.PushAsync(new RecipeDetailPage(recipe, _recipeController));
                }

                glutenFreeCollection.SelectedItem = null;
            }
        }
    }
}
