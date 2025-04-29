using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavorySweets.Project.Models;
using SavorySweets.Project.Data;
using Microsoft.Maui.Storage;

namespace SavorySweets.Project.Controllers
{
    public class UserController
    {
        private readonly UserDataService _userDataService;

        public UserController()
        {
            _userDataService = new UserDataService();
        }

        public bool Register(User newUser)
        {
            return _userDataService.Register(newUser);
        }

        public bool Login(string email, string password)
        {
            var user = _userDataService.Login(email, password);
            if (user != null)
            {
                Preferences.Set("LoggedInUserId", user.ID);
                return true;
            }
            return false;
        }

        public void Logout()
        {
            Preferences.Remove("LoggedInUserId");
        }

        public User? GetLoggedInUser()
        {
            int id = Preferences.Get("LoggedInUserId", -1);
            var users = _userDataService.GetAllUsers();
            return users.FirstOrDefault(u => u.ID == id);
        }


        public void UpdateUserInfo(User updatedUser)
        {
            _userDataService.UpdateUser(updatedUser);
        }
    }
}
