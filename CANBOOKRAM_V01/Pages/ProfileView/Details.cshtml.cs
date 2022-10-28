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

namespace CANBOOKRAM_V01.Pages.ProfileView
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context _context;

        public DetailsModel(CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context context)
        {
            _context = context;
        }
        public bool isFr;
        public int? ide;
        [BindProperty]
        public ProfileDetail ProfileDetail { get; set; } = default!;

        [BindProperty]
        public AspNetUser aaa { get; set; } = default!;

        [BindProperty]
        public List<FriendshipTable> Friends { get; set; } = default!;

        [BindProperty]
        public List<UserPost> UserPost { get; set; } = default!;

        [BindProperty]
        public List<UserPost> UserPostTop5 { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ProfileDetails == null)
            {
                return NotFound();
            }
            ide = id.Value;
            var profiledetail = await _context.ProfileDetails.FirstOrDefaultAsync(m => m.Id == id);
            
            if (profiledetail == null)
            {
                return NotFound();
            }
            else 
            {
                var aspdetail = await _context.AspNetUsers.FirstOrDefaultAsync(m => m.Id == profiledetail.UserId);
                aaa = aspdetail;
                ProfileDetail = profiledetail;
            }
;
            Friends = await _context.FriendshipTables.ToListAsync();
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            UserPost = await _context.UserPosts.Where(u => u.UserId == profiledetail.UserId).ToListAsync();
            UserPostTop5 = (from i in _context.UserPosts
                            where i.IsTrend == 1 && i.UserId == profiledetail.UserId
                            select i).ToList();
            UserPostTop5 = Enumerable.Reverse(UserPostTop5).Take(5).ToList();
            UserPost = Enumerable.Reverse(UserPost).Take(3).ToList();
            foreach (var item in Friends)
            {
                if (item.Friendid == userId)
                {
                    if (item.Userid == profiledetail.UserId)
                    {
                        if(item.Approved == "Approve")
                        {
                            isFr = true;
                            return Page();
                        }
                    }
                }
                if (item.Userid == userId)
                {
                    if (item.Friendid == profiledetail.UserId)
                    {
                        if (item.Approved == "Approve")
                        {
                            isFr = true;
                            return Page();
                        }
                    }
                }
            }
            isFr = false;
            return Page();
        }

        public int? getId()
        {
            return ide;
        }
    }
}
