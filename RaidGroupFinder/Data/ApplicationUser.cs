using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RaidGroupFinder.Data
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(14, ErrorMessage = "Trainer Code is too long.")]
        [RegularExpression("[0-9]{4}[ ][0-9]{4}[ ][0-9]{4}|[0-9]{12}", ErrorMessage = "Invalid Trainer Code")]
        public string TrainerCode { get; set; }
        public string PokemonGoNickname { get; set; }
    }
}
