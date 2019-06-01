using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BusinessApp.API.Models
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}