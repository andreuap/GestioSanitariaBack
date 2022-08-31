using System.ComponentModel.DataAnnotations;

namespace GestioSanitariaAPI.Models.Dto
{
    public class UserCreateDTO
    {
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [MaxLength(255)]
        public string Email { get; set; }
        [Required]
        public DateTime DataAlta { get; set; }
        [Required]
        public int Rol { get; set; }
    }
}
