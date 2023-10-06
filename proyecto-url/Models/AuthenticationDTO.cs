using System.ComponentModel.DataAnnotations;

namespace proyecto_url.Models
{
    public class AuthenticationDTO
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }

    }
}
