using System.Collections.ObjectModel;


namespace SavorySweets.Project.Views
{
    public partial class FiveMinuteRecipesPage : ContentPage
    {
        private readonly RecipeController _recipeController;

        public FiveMinuteRecipesPage()
        {
            InitializeComponent();
            _recipeController = new RecipeController();
            LoadQuickRecipes();
        }

        // Loads recipes categorized as "5-minute"
        private void LoadQuickRecipes()
        {
            var quick = _recipeController.Recipes
                .Where(r =>
                    (r.Category != null && r.Category.ToLower().Contains("5-minute")) ||
                    r.PrepTimeMinutes <= 5)
                .Select(r => new RecipeDisplayView(r))
                .ToList();

            quickRecipesCollection.ItemsSource = new ObservableCollection<RecipeDisplayView>(quick);
        }

        //handles when the favorite icon is clicked to toggle favorite status
        private void OnToggleFavoriteClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.CommandParameter is int id)
            {
                _recipeController.ToggleFavorite(id);
                LoadQuickRecipes(); // Refresh
            }
        }

        //handles when a quick recipe is selected from the CollectionView
        private void OnRecipeSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is RecipeDisplayView selected)
            {
                var recipe = _recipeController.Recipes.FirstOrDefault(r => r.Id == selected.Id);
                if (recipe != null)
                {
                    Navigation.PushAsync(new RecipeDetailPage(recipe, _recipeController));
                }

                quickRecipesCollection.SelectedItem = null;
            }
        }
    }
}

