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

namespace CANBOOKRAM_V01.Pages.Friends
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context _context;

        public EditModel(CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context context)
        {
            _context = context;
        }

        [BindProperty]
        public FriendshipTable UserPost { get; set; } = default!;
        [BindProperty]
        public string Rate { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FriendshipTables == null)
            {
                return NotFound();
            }

            var userpost = await _context.FriendshipTables.FirstOrDefaultAsync(m => m.Id == id);
            if (userpost == null)
            {
                return NotFound();
            }
            UserPost = userpost;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.FriendshipTables == null)
            {
                return Page();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            UserPost.Userid = userId;
            var userpost = await _context.FriendshipTables.FirstOrDefaultAsync(m => m.Userid == userId);
            UserPost.Approved = Rate;
            _context.Attach(UserPost);
            _context.Entry(UserPost).Property(p => p.Approved).IsModified = true;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
