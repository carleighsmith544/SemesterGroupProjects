using System.Globalization;

namespace SavorySweets.Project.Views
{
    public class FavoriteToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isFavorite = value is bool b && b;
            //return filled star if favorite, otherwise outline star
            return isFavorite ? "star_filled.png" : "star_outline.png";
        }

        // always return false
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
