namespace SavorySweets.Project.Controllers
{
    public class ThemeController
    {
        public bool IsDarkMode { get; private set; }    //property to track if dark mode is enabled


        public ThemeController()
        {
            IsDarkMode = false; //default to light mode
        }

        //method to toggle between dark mode and light mode
        public void ToggleTheme()
        {
            IsDarkMode = !IsDarkMode;  //switch the current theme

        }
    }
}
