using System.ComponentModel.DataAnnotations;

namespace PixiuTracker.Forms
{
    public class LoginUserForm
    {
        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
        
    }
}
