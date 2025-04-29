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

            MainPage = new NavigationPage(new HomePage());

        }

    }
}