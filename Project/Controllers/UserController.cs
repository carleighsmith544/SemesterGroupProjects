using SavorySweets.Project.Models;
using SavorySweets.Project.Data;


namespace SavorySweets.Project.Controllers
{
    public class UserController
    {
        private readonly UserDataService _userDataService; //service to interact with user data

        public UserController()
        {
            _userDataService = new UserDataService();
        }

        //registers a new user
        public bool Register(User newUser)
        {
            return _userDataService.Register(newUser);
        }

        //attempts to log a user in with email and password
        public bool Login(string email, string password)
        {
            var user = _userDataService.Login(email, password);
            if (user != null)
            {
                //save the logged-in user's ID 
                Preferences.Set("LoggedInUserId", user.ID);
                return true;
            }
            return false;
        }

        //logs out the current user
        public void Logout()
        {
            Preferences.Remove("LoggedInUserId");
        }

        //retrieves the currently logged-in user's information
        public User? GetLoggedInUser()
        {
            int id = Preferences.Get("LoggedInUserId", -1);
            var users = _userDataService.GetAllUsers();
            return users.FirstOrDefault(u => u.ID == id);
        }

        //updates an existing user's information
        public void UpdateUserInfo(User updatedUser)
        {
            _userDataService.UpdateUser(updatedUser);
        }
    }
}
