using System.Collections.ObjectModel;
using SavorySweets.Project.Models;
using SavorySweets.Project.Data;
using System.Linq;

public class RecipeController
{
    private readonly RecipeDataService _recipeDataService;
    public ObservableCollection<Recipe> Recipes { get; private set; }

    public RecipeController()
    {
        _recipeDataService = new RecipeDataService();
        Recipes = _recipeDataService.LoadRecipes();
    }

    public void AddRecipe(Recipe recipe)
    {
        recipe.Id = Recipes.Count > 0 ? Recipes.Max(r => r.Id) + 1 : 1;
        Recipes.Add(recipe);
        _recipeDataService.SaveRecipes(Recipes);
    }

    public void UpdateRecipe(Recipe updatedRecipe)
    {
        var existing = Recipes.FirstOrDefault(r => r.Id == updatedRecipe.Id);
        if (existing != null)
        {
            existing.Title = updatedRecipe.Title;
            existing.Description = updatedRecipe.Description;
            existing.Ingredients = updatedRecipe.Ingredients;
            existing.Instructions = updatedRecipe.Instructions;
            existing.Category = updatedRecipe.Category;
            existing.PrepTimeMinutes = updatedRecipe.PrepTimeMinutes;
            existing.Imagepath = updatedRecipe.Imagepath;
            existing.IsFavorite = updatedRecipe.IsFavorite;
            _recipeDataService.SaveRecipes(Recipes);
        }
    }

    public void DeleteRecipe(int id)
    {
        var recipe = Recipes.FirstOrDefault(r => r.Id == id);
        if (recipe != null)
        {
            Recipes.Remove(recipe);
            _recipeDataService.SaveRecipes(Recipes);
        }
    }

    public void ToggleFavorite(int id)
    {
        var recipe = Recipes.FirstOrDefault(r => r.Id == id);
        if (recipe != null)
        {
            recipe.IsFavorite = !recipe.IsFavorite;
            _recipeDataService.SaveRecipes(Recipes);
        }
    }

    public ObservableCollection<Recipe> GetUserRecipes(int userId)
    {
        return new ObservableCollection<Recipe>(Recipes.Where(r => r.UserId == userId));
    }

    // ✅ NEW METHOD: Get a recipe by its ID
    public Recipe? GetRecipeById(int id)
    {
        return Recipes.FirstOrDefault(r => r.Id == id);
    }
}
