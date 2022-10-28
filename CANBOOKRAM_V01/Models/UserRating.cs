using System;
using System.Collections.Generic;

namespace CANBOOKRAM_V01.Models
{
    public partial class UserRating
    {
        public int Id { get; set; }
        public int? Rating { get; set; }
        public string? UserId { get; set; }
        public string? Whorated { get; set; }
    }
}
