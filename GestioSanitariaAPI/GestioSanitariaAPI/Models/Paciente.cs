using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestioSanitariaAPI.Models
{
    public class Paciente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? NumeroHistoriaClinica { get; set; }
        public string? Nom { get; set; }
        public string? PrimerCognom { get; set; }
        public string? SegonCognom { get; set; }
        public string? Email { get; set; }
        public int? Edat { get; set; }
        public DateTime? DataNeixament { get; set; }
        public string? Genere { get; set; }
        public byte[]? Foto { get; set; }
        public string? Domicili { get; set; }
        public string? Telefon { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid CreateUserId { get; set; }
        public Guid UpdateUserID { get; set; }


    }
}
