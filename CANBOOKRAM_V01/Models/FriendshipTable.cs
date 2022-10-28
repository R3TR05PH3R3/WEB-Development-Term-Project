using System;
using System.Collections.Generic;

namespace CANBOOKRAM_V01.Models
{
    public partial class FriendshipTable
    {
        public int Id { get; set; }
        public string? Useremail { get; set; }
        public string? Friendemail { get; set; }
        public string? Approved { get; set; }
        public string? Userid { get; set; }
        public string? Friendid { get; set; }
    }
}
