namespace GestioSanitariaAPI.Models.Dto
{
    public class PacientesCreateDTO
    {
        public string? NumeroHistoriaClinica { get; set; }
        public string? Nom { get; set; }
        public string? PrimerCognom { get; set; }
        public string? SegonCognom { get; set; }
        public string? Email { get; set; }
        public int Edat { get; set; }
        public DateTime? DataNeixament { get; set; }
        public string? Genere { get; set; }
        public byte[]? Foto { get; set; }
        public string? Domicili { get; set; }
        public string? Telefon { get; set; }
    }
}
