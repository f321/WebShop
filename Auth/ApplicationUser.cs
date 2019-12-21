using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PieShop.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Birthdate { get; set; }
        public string Country { get; set; }

    public string City { get; set; }
    }
}