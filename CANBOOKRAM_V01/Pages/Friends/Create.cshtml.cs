using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CANBOOKRAM_V01.Models;
using Microsoft.AspNetCore.Authorization;

namespace CANBOOKRAM_V01.Pages.Friends
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context _context;

        public CreateModel(CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context context)
        {
            _context = context;
        }


        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public FriendshipTable FriendshipTable { get; set; } = default!;
        public List<SelectListItem> Users { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.FriendshipTables == null || FriendshipTable == null)
            {
                return Page();
            }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            FriendshipTable.Useremail = User.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            foreach (var item in _context.AspNetUsers)
            {
                if (item.Email == FriendshipTable.Friendemail)
                {
                    FriendshipTable.Friendid = item.Id;
                }
                if (item.Email == FriendshipTable.Useremail)
                {
                    FriendshipTable.Userid = item.Id;
                }
            }
            FriendshipTable.Approved = null;
            
            _context.FriendshipTables.Add(FriendshipTable);


            await _context.SaveChangesAsync();

            return RedirectToPage("/Friends/Index");
        }
    }
}
