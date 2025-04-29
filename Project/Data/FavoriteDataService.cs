using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SavorySweets.Project.Models;

namespace SavorySweets.Project.Data
{
    public class FavoriteDataService
    {
        private readonly string _filePath = Path.Combine(FileSystem.AppDataDirectory, "favorites.json");

        public List<Favorite> LoadFavorites()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Favorite>>(json) ?? new List<Favorite>();
            }
            return new List<Favorite>();
        }

        public void SaveFavorites(List<Favorite> favorites)
        {
            string json = JsonSerializer.Serialize(favorites);
            File.WriteAllText(_filePath, json);
        }
    }
}
