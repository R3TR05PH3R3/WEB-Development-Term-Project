using System;
using System.Collections.Generic;

namespace CANBOOKRAM_V01.Models
{
    public partial class UserPost
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Message { get; set; }
        public byte[]? Picture { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? IsTrend { get; set; }
        public string? UserEmail { get; set; }
        public int? Likes { get; set; }
        public int? Dislikes { get; set; }
    }
}
