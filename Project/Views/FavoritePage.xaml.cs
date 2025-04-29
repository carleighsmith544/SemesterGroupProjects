using SavorySweets.Project.Controllers;
using System.Collections.ObjectModel;

namespace SavorySweets.Project.Views;

public partial class FavoritesPage : ContentPage
{
    private readonly RecipeController _recipeController;
    private readonly FavoriteController _favoriteController;
    private readonly UserController _userController;
    private ObservableCollection<RecipeDisplayView> _favorites = new();

    public FavoritesPage()
    {
        InitializeComponent();
        _recipeController = new RecipeController();
        _favoriteController = new FavoriteController();
        _userController = new UserController();
    }

    //called when the page appears; loads the user's favorites
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadFavoriteRecipes();
    }

    //loads the current user's favorite recipes
    private void LoadFavoriteRecipes()
    {
        var currentUser = _userController.GetLoggedInUser();
        if (currentUser == null)
        {
            //if not logged in, show message
            noFavoritesLabel.Text = "Please sign in to view your favorites.";
            noFavoritesLabel.IsVisible = true;
            favoritesCollection.ItemsSource = null;
            return;
        }

        //retrieve user's favorites
        var userFavorites = _favoriteController
            .GetFavoritesForUser(currentUser.ID)
            .Select(fav => _recipeController.GetRecipeById(fav.RecipeId))
            .Where(recipe => recipe != null)
            .Select(r => new RecipeDisplayView(r))
            .ToList();

        _favorites = new ObservableCollection<RecipeDisplayView>(userFavorites);
        favoritesCollection.ItemsSource = _favorites;

        noFavoritesLabel.IsVisible = _favorites.Count == 0;
    }

    //handles when a favorite recipe is selected from the list
    private void OnFavoriteSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is RecipeDisplayView selected)
        {
            var recipe = _recipeController.GetRecipeById(selected.Id);
            if (recipe != null)
            {
                Navigation.PushAsync(new RecipeDetailPage(recipe, _recipeController));
            }
            ((CollectionView)sender).SelectedItem = null;
        }
    }

    //handles when the favorite icon is clicked to toggle favorite status
    private void OnToggleFavoriteClicked(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        int recipeId = (int)button.CommandParameter;

        var user = _userController.GetLoggedInUser();
        if (user == null)
        {
            DisplayAlert("Not Logged In", "Please log in to favorite recipes.", "OK");
            return;
        }

        //toggle favorite status
        if (_favoriteController.IsFavorited(user.ID, recipeId))
        {
            _favoriteController.RemoveFavorite(user.ID, recipeId);
        }
        else
        {
            _favoriteController.AddFavorite(user.ID, recipeId);
        }

        LoadFavoriteRecipes(); //refresh the list
    }
}
