using System.Collections.ObjectModel;

namespace SavorySweets.Project.Views
{
    public partial class BeginnerRecipesPage : ContentPage
    {
        private readonly RecipeController _recipeController;

        public BeginnerRecipesPage()
        {
            InitializeComponent();
            _recipeController = new RecipeController();
            LoadBeginnerRecipes();
        }
        //loads recipes that belong to the "Beginner" category
        private void LoadBeginnerRecipes()
        {
            var beginnerRecipes = _recipeController.Recipes
                .Where(r => r.Category != null && r.Category.ToLower().Contains("beginner"))
                .Select(r => new RecipeDisplayView(r))
                .ToList();

            beginnerRecipesCollection.ItemsSource = new ObservableCollection<RecipeDisplayView>(beginnerRecipes);
        }

        //handles when the favorite icon is clicked to toggle favorite status
        private void OnToggleFavoriteClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.CommandParameter is int id)
            {
                _recipeController.ToggleFavorite(id);
                LoadBeginnerRecipes(); // Refresh list
            }
        }

        //handles when a recipe item is selected from the collection view
        private void OnRecipeSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is RecipeDisplayView selected)
            {
                var recipe = _recipeController.Recipes.FirstOrDefault(r => r.Id == selected.Id);
                if (recipe != null)
                {
                    Navigation.PushAsync(new RecipeDetailPage(recipe, _recipeController));
                }

                beginnerRecipesCollection.SelectedItem = null;
            }
        }
    }
}
