using SavorySweets.Project.Models;
using SavorySweets.Project.Controllers;
using Microsoft.Maui.Controls;

namespace SavorySweets.Project.Views;

public partial class AddOrEditRecipePage : ContentPage
{
    private readonly RecipeController _recipeController;
    private readonly UserController _userController;
    private readonly Recipe? _recipeToEdit;
    private string _imagePath = "";

    public AddOrEditRecipePage(Recipe? recipe, RecipeController recipeController, UserController userController)
    {
        InitializeComponent();
        _recipeToEdit = recipe;
        _recipeController = recipeController;
        _userController = userController;

        Title = _recipeToEdit != null ? "Edit Recipe" : "Add Recipe";

        if (_recipeToEdit != null)
        {
            LoadRecipeDetails();
        }
    }

    private void LoadRecipeDetails()
    {
        titleEntry.Text = _recipeToEdit.Title;
        descriptionEditor.Text = _recipeToEdit.Description;
        ingredientsEditor.Text = _recipeToEdit.Ingredients;
        instructionsEditor.Text = _recipeToEdit.Instructions;
        prepTimeEntry.Text = _recipeToEdit.PrepTimeMinutes.ToString();
        categoryPicker.SelectedItem = string.IsNullOrWhiteSpace(_recipeToEdit.Category) ? "No Category" : _recipeToEdit.Category;
        _imagePath = _recipeToEdit.Imagepath;
        recipeImage.Source = string.IsNullOrEmpty(_imagePath) ? "default_recipe.png" : _imagePath;
    }

    private async void OnChooseImageClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions{PickerTitle = "Select a recipe image", FileTypes = FilePickerFileType.Images});

            if (result != null)
            {
                _imagePath = result.FullPath;
                recipeImage.Source = ImageSource.FromFile(_imagePath);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not pick image: {ex.Message}", "OK");
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(titleEntry.Text) ||
            string.IsNullOrWhiteSpace(ingredientsEditor.Text) ||
            string.IsNullOrWhiteSpace(instructionsEditor.Text) ||
            string.IsNullOrWhiteSpace(prepTimeEntry.Text))
        {
            await DisplayAlert("Missing Info", "Please fill in all required fields.", "OK");
            return;
        }

        if (!int.TryParse(prepTimeEntry.Text, out int prepTime))
        {
            await DisplayAlert("Invalid Input", "Prep time must be a number.", "OK");
            return;
        }

        var currentUser = _userController.GetLoggedInUser();
        if (currentUser == null)
        {
            await DisplayAlert("Error", "You must be logged in to save a recipe.", "OK");
            return;
        }

        string selectedCategory = categoryPicker.SelectedItem?.ToString();
        string finalCategory = selectedCategory == "No Category" ? "" : selectedCategory;

        if (_recipeToEdit == null)
        {
            var newRecipe = new Recipe
            {
                Id = 0,
                Title = titleEntry.Text,
                Description = descriptionEditor.Text,
                Ingredients = ingredientsEditor.Text,
                Instructions = instructionsEditor.Text,
                PrepTimeMinutes = prepTime,
                Category = finalCategory,
                Imagepath = _imagePath,
                UserId = currentUser.ID
            };

            _recipeController.AddRecipe(newRecipe);
        }
        else
        {
            _recipeToEdit.Title = titleEntry.Text;
            _recipeToEdit.Description = descriptionEditor.Text;
            _recipeToEdit.Ingredients = ingredientsEditor.Text;
            _recipeToEdit.Instructions = instructionsEditor.Text;
            _recipeToEdit.PrepTimeMinutes = prepTime;
            _recipeToEdit.Category = finalCategory;
            _recipeToEdit.Imagepath = _imagePath;

            _recipeController.UpdateRecipe(_recipeToEdit);
        }

        await DisplayAlert("Success", "Recipe saved successfully!", "OK");
        await Navigation.PopAsync();
    }
}
