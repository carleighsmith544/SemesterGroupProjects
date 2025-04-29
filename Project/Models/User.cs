using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavorySweets.Project.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string ProfileImagePath { get; set; } = "";
        public bool IsDarkMode { get; set; } = false;
    }
}
