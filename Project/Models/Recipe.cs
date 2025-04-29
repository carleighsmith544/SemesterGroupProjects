namespace SavorySweets.Project.Models
{
    public class Recipe
    {
        public int Id { get; set; } //unique id for recipe
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Ingredients { get; set; } = "";
        public string Instructions { get; set; } = "";  
        public string Category { get; set; } = "";
        public int PrepTimeMinutes { get; set; }
        public bool IsFavorite { get; set; }
        public string Imagepath { get; set; } = "";
        public int UserId { get; set; }
    }
}
