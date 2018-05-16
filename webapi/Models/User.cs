using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime? Created { get; set; }
        public bool Is_Activated { get; set; }
        public bool Is_Disabled { get; set; }
    }
}
