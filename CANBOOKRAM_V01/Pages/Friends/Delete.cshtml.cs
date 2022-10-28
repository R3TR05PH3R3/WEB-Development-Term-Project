using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CANBOOKRAM_V01.Models;
using Microsoft.AspNetCore.Authorization;

namespace CANBOOKRAM_V01.Pages.Friends
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context _context;

        public DeleteModel(CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context context)
        {
            _context = context;
        }

        [BindProperty]
      public FriendshipTable FriendshipTable { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FriendshipTables == null)
            {
                return NotFound();
            }

            var friendshiptable = await _context.FriendshipTables.FirstOrDefaultAsync(m => m.Id == id);

            if (friendshiptable == null)
            {
                return NotFound();
            }
            else 
            {
                FriendshipTable = friendshiptable;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.FriendshipTables == null)
            {
                return NotFound();
            }
            var friendshiptable = await _context.FriendshipTables.FindAsync(id);

            if (friendshiptable != null)
            {
                FriendshipTable = friendshiptable;
                _context.FriendshipTables.Remove(FriendshipTable);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
