// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CANBOOKRAM_V01.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace CANBOOKRAM_V01.Areas.Identity.Pages.Account.Manage
{
    public class setHiddenModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly CANBOOKRAM_V01Context _context;

        public setHiddenModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            CANBOOKRAM_V01Context context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public ProfileDetail UserDetails { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>


        private async Task LoadAsync(IdentityUser user)
        {
            UserDetails = _context.ProfileDetails.Where(p => p.UserId == user.Id).FirstOrDefault();
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
            UserDetails = _context.ProfileDetails.Where(p => p.UserId == user.Id).FirstOrDefault();

            if (UserDetails.Hidden == 1)
            {
                UserDetails.Hidden = 0;
                UserDetails.UserId = user.Id;

                _context.ProfileDetails.Update(UserDetails);
                await _context.SaveChangesAsync();
                StatusMessage = "Your Hidden Profile Info is changed.";

                return RedirectToPage();
            }
            else if(UserDetails.Hidden == 0);
            {
                UserDetails.Hidden = 1;
                UserDetails.UserId = user.Id;

                _context.ProfileDetails.Update(UserDetails);
                await _context.SaveChangesAsync();
                StatusMessage = "Your Hidden Profile Info is changed.";

                return RedirectToPage();
            }
            return RedirectToPage();
        }


    }
}
