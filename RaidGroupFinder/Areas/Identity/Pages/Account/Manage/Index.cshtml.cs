using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RaidGroupFinder.Data;

namespace RaidGroupFinder.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }
        public List<SelectListItem> Options { get; set; }
        
        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [StringLength(14, ErrorMessage = "Trainer Code is too long.")]
            [RegularExpression("[0-9]{4}[ ][0-9]{4}[ ][0-9]{4}|[0-9]{12}", ErrorMessage = "Invalid Trainer Code")]
            public string TrainerCode { get; set; }
            [Required]
            [StringLength(15, ErrorMessage = "Trainer nickname is too long.")]
            public string PokemonGoNickname { get; set; }
            [Required]
            public string Timezone { get; set; }
        }


        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            Username = userName;
           
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

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (Input.TrainerCode != user.TrainerCode || Input.PokemonGoNickname != user.PokemonGoNickname)
            {
                user.PokemonGoNickname = Input.PokemonGoNickname;
                user.TrainerCode = Input.TrainerCode;
                if (!TryValidateModel(user)) 
                {
                    StatusMessage = "Error!";
                    return Page();
                }

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
