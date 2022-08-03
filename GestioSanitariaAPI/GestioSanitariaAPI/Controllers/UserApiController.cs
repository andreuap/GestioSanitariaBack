using GestioSanitariaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestioSanitariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return new List<User>
            {
                new User { Id = new Guid(), UserName = "andreu", Password = "123456", Email = "andreu@example.com", DataAlta = new DateTime(), DataBaixa = null, EsBloquejat = false },
                new User { Id = new Guid(), UserName = "anna", Password = "7895", Email = "anna@example.com", DataAlta = new DateTime(), DataBaixa = null, EsBloquejat = false }
            };
        }
    }
}
