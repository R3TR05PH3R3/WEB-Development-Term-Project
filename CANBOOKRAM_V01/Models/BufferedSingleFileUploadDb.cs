using System.ComponentModel.DataAnnotations;
using CANBOOKRAM_V01.Models;



namespace CANBOOKRAM_V01.Models
{
    public class BufferedSingleFileUploadDb
    {
        [Required]
        [Display(Name = "Profile Picture")]
        public IFormFile? FormFile { get; set; }
    }
}
