using SavorySweets.Project.Models;


namespace SavorySweets.Project.Controllers
{
    //controller class
    public class FavoriteController
    {
        //static list to store all favorite entries
        private static List<Favorite> _favorites = new();
        
        //adds a recipe to a user's list of favorites if it is not already favorited

        public void AddFavorite(int userId, int recipeId)
        {
            //if the favorite already exists, avoid a duplicatiion
            if (!_favorites.Any(f => f.UserId == userId && f.RecipeId == recipeId))
            {
                //add new favorite
                _favorites.Add(new Favorite
                {
                    Id = _favorites.Count + 1,
                    UserId = userId,
                    RecipeId = recipeId
                });
            }
        }

        //function to remove favorite from list
        public void RemoveFavorite(int userId, int recipeId)
        {
            var favorite = _favorites.FirstOrDefault(f => f.UserId == userId && f.RecipeId == recipeId);
            //if favorite exists, remove it
            if (favorite != null)
            {
                _favorites.Remove(favorite);
            }
        }

        //function to check if a specific recipe is favorited by a specific user
        public bool IsFavorited(int userId, int recipeId)
        {
            return _favorites.Any(f => f.UserId == userId && f.RecipeId == recipeId);
        }

        //function to retrieve all the favorite recipes 
        public List<Favorite> GetFavoritesForUser(int userId)
        {
            return _favorites.Where(f => f.UserId == userId).ToList();
        }
    }
}

