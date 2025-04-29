using System.Collections.ObjectModel;
using System.Text.Json;
using SavorySweets.Project.Models;

namespace SavorySweets.Project.Data
{
    public class RecipeDataService
    {
        private readonly string filePath; //file path to store the recipes JSON file

        public RecipeDataService()
        {
            filePath = Path.Combine(FileSystem.AppDataDirectory, "recipes.json");
        }

        public ObservableCollection<Recipe> LoadRecipes()
        {
            //check if the recipes file exists
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var recipes = JsonSerializer.Deserialize<List<Recipe>>(json);
                return new ObservableCollection<Recipe>(recipes ?? new List<Recipe>());
            }

            return new ObservableCollection<Recipe>();
        }

        //saves ObservableCollection of recipes to the JSON file
        public void SaveRecipes(ObservableCollection<Recipe> recipes)
        {
            string json = JsonSerializer.Serialize(recipes.ToList(), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
