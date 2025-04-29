using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using SavorySweets.Project.Models;


namespace SavorySweets.Project.Controllers
{

    public class FavoriteController
    {
        private static List<Favorite> _favorites = new();
        public void AddFavorite(int userId, int recipeId)
        {
            if (!_favorites.Any(f => f.UserId == userId && f.RecipeId == recipeId))
            {
                _favorites.Add(new Favorite
                {
                    Id = _favorites.Count + 1,
                    UserId = userId,
                    RecipeId = recipeId
                });
            }
        }

        public void RemoveFavorite(int userId, int recipeId)
        {
            var favorite = _favorites.FirstOrDefault(f => f.UserId == userId && f.RecipeId == recipeId);
            if (favorite != null)
            {
                _favorites.Remove(favorite);
            }
        }

        public bool IsFavorited(int userId, int recipeId)
        {
            return _favorites.Any(f => f.UserId == userId && f.RecipeId == recipeId);
        }

        public List<Favorite> GetFavoritesForUser(int userId)
        {
            return _favorites.Where(f => f.UserId == userId).ToList();
        }
    }
}

