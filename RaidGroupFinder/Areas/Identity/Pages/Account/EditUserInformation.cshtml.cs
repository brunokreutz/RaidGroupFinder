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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using RaidGroupFinder.Data;
using RaidGroupFinder.Helper;

namespace RaidGroupFinder.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class EditUserInformationModel : PageModel
    {

        [BindProperty]
        public InputModel Input { get; set; }

        private readonly UserManager<ApplicationUser> _userManager;
        public List<SelectListItem> Options { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(14, ErrorMessage = "Trainer Code is too long.")]
            [RegularExpression("[0-9]{4}[ ][0-9]{4}[ ][0-9]{4}|[0-9]{12}", ErrorMessage = "Invalid Trainer Code!")]
            public string TrainerCode { get; set; }
            [Required]
            [StringLength(15, ErrorMessage = "Trainer nickname is too long.")]
            public string PokemonGoNickname { get; set; }
            [Required]
            public string Timezone { get; set; }
        }


        public EditUserInformationModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public string StatusMessage { get; set; }
        public string Alert { get; set; }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user); 

            Input = new InputModel
            {
                PokemonGoNickname = user.PokemonGoNickname,
                TrainerCode = user.TrainerCode
            };

            var tzs = TimeZoneInfo.GetSystemTimeZones();
            Options = tzs.Select(tz => new SelectListItem()
            {
                Text = tz.DisplayName,
                Value = tz.Id
            }).ToList();
        }

        public async Task<ActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

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
            user.TrainerCode = RegexHelper.ReplaceWhitespace(Input.TrainerCode);
            user.TimeZone = Input.Timezone;
            
            if (!TryValidateModel(user))
            {
                StatusMessage = "Error! Invalid Trainer code";
                return await this.OnGetAsync();
            }
            var result = await _userManager.UpdateAsync(user);
            StatusMessage = result.Succeeded ? "Trainer info Updated" : "Error updating your Trainer Info.";
            
            return await this.OnGetAsync();
        }
    }
}
