using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RaidGroupFinder.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string TrainerCode { get; set; }
        public string PokemonGoNickname { get; set; }
    }
}
