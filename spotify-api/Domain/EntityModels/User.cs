using Microsoft.AspNetCore.Identity;
using SpotifyApi.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Models
{
    public class User : IdentityUser<int>
    {

        public string Password { get; set; }

        public string Href { get; set; }

        //AddRoles to our User
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
