﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RaidGroupFinder.Data
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(14, ErrorMessage = "Trainer Code is too long.")]
        [RegularExpression("[0-9]{4}[ ][0-9]{4}[ ][0-9]{4}|[0-9]{12}", ErrorMessage = "Invalid Trainer Code")]
        public string TrainerCode { get; set; }
        [StringLength(15, ErrorMessage = "Nickname is too long or too small.", MinimumLength = 1)]
        public string PokemonGoNickname { get; set; }
        [StringLength(50, ErrorMessage = "Timezone is too long or too small.", MinimumLength = 1)]
        public string TimeZone { get; set; }
    }
}
