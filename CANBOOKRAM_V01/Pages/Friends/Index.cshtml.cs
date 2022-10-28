using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CANBOOKRAM_V01.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CANBOOKRAM_V01.Pages.Friends
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context _context;
        public IndexModel(CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context context)
        {
            _context = context;
        }

        public IList<FriendshipTable> FriendshipTable { get;set; } = default!;
        public AspNetUser aspUsr { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.FriendshipTables != null)
            {
                FriendshipTable = await _context.FriendshipTables.ToListAsync();
            }
        }
    }
}
