using System.ComponentModel.DataAnnotations;

namespace BookSrote.API.Models
{
    public class SignUpModel
    {
        [Required]
        public string Firstname { get; set; } = string.Empty;
        [Required]
        public string Lastname { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Compare("ConfirmPassword")]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
