using System.Collections.ObjectModel;


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

        // Loads recipes that are categorized as "Gluten-Free"
        private void LoadGlutenFreeRecipes()
        {
            var glutenFree = _recipeController.Recipes
                .Where(r => r.Category != null && r.Category.ToLower().Contains("gluten-free"))
                .Select(r => new RecipeDisplayView(r))
                .ToList();

            glutenFreeCollection.ItemsSource = new ObservableCollection<RecipeDisplayView>(glutenFree);
        }

        //handles when the favorite icon is clicked to toggle favorite status
        private void OnToggleFavoriteClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.CommandParameter is int id)
            {
                _recipeController.ToggleFavorite(id);
                LoadGlutenFreeRecipes(); // Refresh
            }
        }

        //handles when a gluten-free recipe is selected from the CollectionView
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
