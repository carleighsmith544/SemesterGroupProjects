using SavorySweets.Project.Models;
using SavorySweets.Project.Controllers;
using System.Collections.ObjectModel;

namespace SavorySweets.Project.Views;

public partial class RecipeDetailPage : ContentPage
{
    private readonly Recipe _recipe;
    private readonly RecipeController _recipeController;
    private readonly FavoriteController _favoriteController = new();
    private readonly UserController _userController = new();

    public RecipeDetailPage(Recipe recipe, RecipeController controller)
    {
        InitializeComponent();
        _recipe = recipe;
        _recipeController = controller;

        titleLabel.Text = _recipe.Title;
        descriptionLabel.Text = _recipe.Description;
        ingredientsLabel.Text = _recipe.Ingredients;
        instructionsLabel.Text = _recipe.Instructions;
        prepTimeLabel.Text = $"Prep Time: {_recipe.PrepTimeMinutes} minutes";
        categoryLabel.Text = $"Category: {_recipe.Category}";

        recipeImage.Source = string.IsNullOrEmpty(_recipe.Imagepath)
            ? "default_recipe.png"
            : _recipe.Imagepath;

        SetFavoriteIcon();
    }

    private void SetFavoriteIcon()
    {
        var user = _userController.GetLoggedInUser();
        if (user != null && _favoriteController.IsFavorited(user.ID, _recipe.Id))
        {
            favoriteButton.Source = "star_filled.png";
        }
        else
        {
            favoriteButton.Source = "star_outline.png";
        }
    }

    private void OnFavoriteClicked(object sender, EventArgs e)
    {
        var user = _userController.GetLoggedInUser();
        if (user == null)
        {
            DisplayAlert("Not Logged In", "Please log in to favorite recipes.", "OK");
            return;
        }

        if (_favoriteController.IsFavorited(user.ID, _recipe.Id))
        {
            _favoriteController.RemoveFavorite(user.ID, _recipe.Id);
        }
        else
        {
            _favoriteController.AddFavorite(user.ID, _recipe.Id);
        }

        SetFavoriteIcon();
    }
}
