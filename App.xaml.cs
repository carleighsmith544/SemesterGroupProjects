using SavorySweets.Project.Views;
using SavorySweets.Project.Controllers;
using SavorySweets.Project.Models;

namespace SavorySweets
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            // Set the initial page to the HomePage wrapped in a NavigationPage
            MainPage = new NavigationPage(new HomePage());

        }

    }
}
