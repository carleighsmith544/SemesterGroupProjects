using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavorySweets.Project.Controllers
{
    public class ThemeController
    {
        public bool IsDarkMode { get; private set; }

        public ThemeController()
        {
            IsDarkMode = false; // default
        }

        public void ToggleTheme()
        {
            IsDarkMode = !IsDarkMode;

            // Optional: Apply platform-specific theme here
            // App.Current.UserAppTheme = IsDarkMode ? AppTheme.Dark : AppTheme.Light;
        }
    }
}
