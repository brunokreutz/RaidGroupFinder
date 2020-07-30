using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using RaidGroupFinder.Data;

namespace RaidGroupFinder.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class EditUserInformationModel : PageModel
    {

        [BindProperty]
        public InputModel Input { get; set; }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public class InputModel
        {
            [Required]
            [StringLength(14, ErrorMessage = "Trainer Code is too long.")]
            [RegularExpression("[0-9]{4}[ ][0-9]{4}[ ][0-9]{4}|[0-9]{12}", ErrorMessage = "Invalid Trainer Code!")]
            public string TrainerCode { get; set; }
            [Required]
            public string PokemonGoNickname { get; set; }
        }

        public EditUserInformationModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string StatusMessage { get; set; }
        public string Alert { get; set; }

        //public async Task<IActionResult> OnGetAsync()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //    }
        //    return Page();
        //}

        public async Task<ActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            user.PokemonGoNickname = Input.PokemonGoNickname;
            user.TrainerCode = Input.TrainerCode;
            if (!TryValidateModel(user))
            {
                StatusMessage = "Error! Invalid Trainer code";
                return Page();
            }
            var result = await _userManager.UpdateAsync(user);
            StatusMessage = result.Succeeded ? "Trainer info Updated" : "Error updating your Trainer Info.";
            
            return Page();
        }
    }
}
