using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavorySweets.Project.Models;

namespace SavorySweets.Project.Views
{
    public class RecipeDisplayView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Imagepath { get; set; }
        public bool IsFavorite { get; set; }

        public RecipeDisplayView(Recipe recipe)
        {
            Id = recipe.Id;
            Title = recipe.Title;
            Description = recipe.Description;
            Imagepath = recipe.Imagepath ?? "default_recipe.png";
        }
    }
}

