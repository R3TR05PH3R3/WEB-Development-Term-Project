using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CANBOOKRAM_V01.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CANBOOKRAM_V01.Pages.post
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context _context;
        private readonly UserManager<IdentityUser> _userManager;



        public CreateModel(CANBOOKRAM_V01.Models.CANBOOKRAM_V01Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserPost UserPost { get; set; } = default!;
        [BindProperty]
        public MessagePictureFile? FileUpload { get; set; }

        /*public int MaxNo()
        {
            using(var context = new CANBOOKRAM_V01Context())
            {
                var no = (from x in context.UserPosts select x.Id).Max();
                if (context.UserPosts == null)
                {
                    no = 0;
                }
                return no;
            }
        }*/
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //int number = MaxNo();
            if (!ModelState.IsValid || _context.UserPosts == null || UserPost == null)
            {
                return Page();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            UserPost.UserId = userId;

            if (FileUpload.FormFile != null)
            {
                //UserPost.Id = number + 1;
                var memoryStream = new MemoryStream();
                await FileUpload.FormFile.CopyToAsync(memoryStream);
                UserPost.Picture = memoryStream.ToArray();
            }
            else { UserPost.Picture = null; }
            foreach(var item in _context.AspNetUsers)
            {
                if(item.Id == UserPost.UserId)
                {
                    UserPost.UserEmail = item.Email;
                }
            }
            UserPost.IsTrend = UserPost.IsTrend;
            UserPost.Timestamp = DateTime.Now;
            UserPost.Likes = 0;
            UserPost.Dislikes = 0;
            _context.UserPosts.Add(UserPost);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
