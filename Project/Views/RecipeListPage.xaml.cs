using SavorySweets.Project.Controllers;
using System.Collections.ObjectModel;

namespace SavorySweets.Project.Views;

public partial class RecipeListPage : ContentPage
{
    private readonly RecipeController _recipeController;     
    private readonly FavoriteController _favoriteController;
    private readonly UserController _userController;         
    private ObservableCollection<RecipeDisplayView> _recipes; 

    public RecipeListPage()
    {
        InitializeComponent();
        _recipeController = new RecipeController();
        _favoriteController = new FavoriteController();
        _userController = new UserController();
        LoadRecipes();
    }

    // Loads all recipes and updates their favorite status for the logged-in user
    private void LoadRecipes()
    {
        var user = _userController.GetLoggedInUser();
        if (user == null)
        {
            DisplayAlert("Not Logged In", "Please sign in to view recipes.", "OK");
            return;
        }

        _recipes = new ObservableCollection<RecipeDisplayView>(
            _recipeController.Recipes.Select(r =>
            {
                var view = new RecipeDisplayView(r);
                view.IsFavorite = _favoriteController.IsFavorited(user.ID, r.Id);
                return view;
            }));

        recipeCollectionView.ItemsSource = _recipes;
    }

    // Handles when a recipe is tapped (selected) and navigates to the detail page
    private void OnRecipeTapped(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is RecipeDisplayView selectedView)
        {
            var recipe = _recipeController.GetRecipeById(selectedView.Id);
            if (recipe != null)
            {
                Navigation.PushAsync(new RecipeDetailPage(recipe, _recipeController));
            }

            ((CollectionView)sender).SelectedItem = null; 
        }
    }

    // Handles when the favorite button is clicked to toggle favorite status
    private void OnFavoriteClicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.CommandParameter is int recipeId)
        {
            var user = _userController.GetLoggedInUser();
            if (user == null)
            {
                DisplayAlert("Not Logged In", "Please log in to favorite recipes.", "OK");
                return;
            }

            if (_favoriteController.IsFavorited(user.ID, recipeId))
            {
                _favoriteController.RemoveFavorite(user.ID, recipeId);
            }
            else
            {
                _favoriteController.AddFavorite(user.ID, recipeId);
            }

            LoadRecipes(); 
    }
}
