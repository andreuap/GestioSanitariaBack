namespace GestioSanitariaAPI.Models.Dto
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DataAlta { get; set; }
        public DateTime? DataBaixa { get; set; }
        public bool EsBloquejat { get; set; }
    }
}
