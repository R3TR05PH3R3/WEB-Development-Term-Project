using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CANBOOKRAM_V01.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CANBOOKRAM_V01.Pages.post
{
    [Authorize]
    public class AddModel : PageModel
    {
        private readonly CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context _context;

        public AddModel(CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context context)
        {
            _context = context;
        }

        [BindProperty]
        public UserPost UserPost { get; set; }
        [BindProperty]
        public string Rate { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserPosts == null)
            {
                return NotFound();
            }

            var userpost = await _context.UserPosts.FirstOrDefaultAsync(m => m.Id == id);
            if (userpost == null)
            {
                return NotFound();
            }
            UserPost = userpost;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid || _context.UserPosts == null)
            {
                return Page();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            UserPost.UserId = userId;
            var userpost = await _context.UserPosts.FirstOrDefaultAsync(m => m.Id == id);
            if (Rate == "Like")
            {
                userpost.Likes++;
                _context.Attach(userpost);
                _context.Entry(userpost).Property(p => p.Likes).IsModified = true;
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            else if(Rate == "Dislike"){
                userpost.Dislikes++;
                _context.Attach(userpost);
                _context.Entry(userpost).Property(p => p.Dislikes).IsModified = true;
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }


            return Page();
        }
    }
}
