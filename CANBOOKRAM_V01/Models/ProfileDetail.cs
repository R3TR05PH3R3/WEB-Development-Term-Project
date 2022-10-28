using System;
using System.Collections.Generic;

namespace CANBOOKRAM_V01.Models
{
    public partial class ProfileDetail
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Hidden { get; set; }
        public string? Email { get; set; }
    }
}
