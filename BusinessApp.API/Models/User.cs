using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BusinessApp.API.Models
{
    public class User : IdentityUser<int>
    {
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public DateTime ValidTill { get; set; }
        public ICollection<PaytmOrder> PaytmOrders { get; set; }
        public ICollection<Business> Businesses { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}