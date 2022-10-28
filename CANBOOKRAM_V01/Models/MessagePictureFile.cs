using System.ComponentModel.DataAnnotations;

namespace CANBOOKRAM_V01.Models
{
    public class MessagePictureFile
    {
        [Display(Name = "Picture")]
        public IFormFile? FormFile { get; set; }
    }
}

