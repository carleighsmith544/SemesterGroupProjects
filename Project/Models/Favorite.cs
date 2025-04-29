namespace SavorySweets.Project.Models
{
    public class Favorite
    {
        public int Id { get; set; } //unique ID for favorite
        public int UserId { get; set; } //id for user
        public int RecipeId { get; set; } //id for recipe
    }
}
