using System.ComponentModel.DataAnnotations;

namespace GestioSanitariaAPI.Models.Dto
{
    public class UserDTO
    {
        public Guid Id { get; set; }
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
        public DateTime? DataBaixa { get; set; }
        [Required]
        public bool EsBloquejat { get; set; }
    }
}
