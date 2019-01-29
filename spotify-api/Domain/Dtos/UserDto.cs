using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Dtos
{
    public class UserDto : LinkedResourceBaseDto
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
