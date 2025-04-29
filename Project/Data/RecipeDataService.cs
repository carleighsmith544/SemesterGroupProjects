using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SavorySweets.Project.Models;

namespace SavorySweets.Project.Data
{
    public class RecipeDataService
    {
        private readonly string filePath;

        public RecipeDataService()
        {
            filePath = Path.Combine(FileSystem.AppDataDirectory, "recipes.json");
        }

        public ObservableCollection<Recipe> LoadRecipes()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var recipes = JsonSerializer.Deserialize<List<Recipe>>(json);
                return new ObservableCollection<Recipe>(recipes ?? new List<Recipe>());
            }

            return new ObservableCollection<Recipe>();
        }

        public void SaveRecipes(ObservableCollection<Recipe> recipes)
        {
            string json = JsonSerializer.Serialize(recipes.ToList(), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
