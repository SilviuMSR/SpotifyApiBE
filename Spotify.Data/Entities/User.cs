using System;
using System.Collections.Generic;
using System.Text;

namespace Spotify.Data.Entities
{
    public class User : IdentityUser<int>
    {
        public string Country { get; set; }

        public string Password { get; set; }

        public string Href { get; set; }

        //AddRoles to our User
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
