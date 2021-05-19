using System.Globalization;
using Microsoft.AspNetCore.Identity;

namespace TaskSchedulerWebApp.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }
}
