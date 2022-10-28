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

namespace CANBOOKRAM_V01.Pages.post
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [BindProperty]
        public List<UserPost> UserPost { get; set; } = default!;

        [BindProperty]
        public ProfileDetail ProfileDetail { get; set; } = default!;

        [BindProperty]
        public List<FriendshipTable> Friends { get; set; } = default!;
        public int isFr;
        public string userId;
        public async Task OnGetAsync()
        {
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            foreach (var post in _context.UserPosts)
            {
                if (post.Likes - post.Dislikes > 0)
                {
                    post.IsTrend = 1;
                    _context.Attach(post);
                    _context.Entry(post).Property(p => p.IsTrend).IsModified = true;
                }
                else
                {
                    post.IsTrend = 0;
                    _context.Attach(post);
                    _context.Entry(post).Property(p => p.IsTrend).IsModified = true;
                }
            }
            UserPost = await _context.UserPosts.Where(u => u.UserId != null).ToListAsync();
            UserPost.Reverse();
            Friends = await _context.FriendshipTables.ToListAsync();
            await _context.SaveChangesAsync();
        }
    }
}
