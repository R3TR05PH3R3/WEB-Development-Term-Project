using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CANBOOKRAM_V01.Models;

namespace CANBOOKRAM_V01.Pages.ProfileView
{
    public class IndexModel : PageModel
    {
        private readonly CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context _context;

        public IndexModel(CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context context)
        {
            _context = context;
        }

        public IList<ProfileDetail> ProfileDetail { get;set; } = default!;
        public IList<AspNetUser> AspNetUser { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (_context.ProfileDetails != null)
            {
                ProfileDetail = await _context.ProfileDetails.ToListAsync();
                AspNetUser = await _context.AspNetUsers.ToListAsync();
            }
        }
    }
}
