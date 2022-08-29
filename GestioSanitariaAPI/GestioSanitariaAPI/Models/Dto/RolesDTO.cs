using System.ComponentModel.DataAnnotations;

namespace GestioSanitariaAPI.Models.Dto
{
    public class RolesDTO
    {
        int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Rol { get; set; }
    }
}
