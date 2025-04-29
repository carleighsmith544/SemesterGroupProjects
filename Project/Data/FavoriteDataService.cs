using System.Text.Json;
using SavorySweets.Project.Models;

namespace SavorySweets.Project.Data
{
    public class FavoriteDataService
    {
        //file path to store the favorites JSON file
        private readonly string _filePath = Path.Combine(FileSystem.AppDataDirectory, "favorites.json");

        //loads the list of favorites from the JSON file
        public List<Favorite> LoadFavorites()
        {
            //checks if the file exists
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Favorite>>(json) ?? new List<Favorite>();
            }
            //if file doesn't exist, return empty list
            return new List<Favorite>();
        }

        //saves the list of favorites to the JSON file
        public void SaveFavorites(List<Favorite> favorites)
        {
            string json = JsonSerializer.Serialize(favorites);
            File.WriteAllText(_filePath, json);
        }
    }
}
