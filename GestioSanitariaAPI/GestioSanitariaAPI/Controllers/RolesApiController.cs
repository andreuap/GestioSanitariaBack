using GestioSanitariaAPI.Data;
using GestioSanitariaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GestioSanitariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public RolesApiController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<RolesDTO>> GetUsers()
        {
            var roles = _db.Roles.ToList();
            return Ok(roles);
        }
    }
}
