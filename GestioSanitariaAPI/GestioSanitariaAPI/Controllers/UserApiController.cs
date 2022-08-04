using GestioSanitariaAPI.Models;
using GestioSanitariaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GestioSanitariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            return Ok(new List<UserDTO>
            {
                new UserDTO { Id = new Guid("f6f020cc-edb7-4809-bf60-8f42b3e67932"), UserName = "andreu", Password = "123456", Email = "andreu@example.com", DataAlta = new DateTime(), DataBaixa = null, EsBloquejat = false },
                new UserDTO { Id = new Guid("523e1b90-fad9-41be-9f63-0848d2471b30"), UserName = "anna", Password = "7895", Email = "anna@example.com", DataAlta = new DateTime(), DataBaixa = null, EsBloquejat = false }
            });
        }
        [HttpGet("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<UserDTO> GetUser(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return BadRequest();
            }

            var user = new List<UserDTO>
            {
                new UserDTO { Id = new Guid("f6f020cc-edb7-4809-bf60-8f42b3e67932"), UserName = "andreu", Password = "123456", Email = "andreu@example.com", DataAlta = new DateTime(), DataBaixa = null, EsBloquejat = false },
                new UserDTO { Id = new Guid("523e1b90-fad9-41be-9f63-0848d2471b30"), UserName = "anna", Password = "7895", Email = "anna@example.com", DataAlta = new DateTime(), DataBaixa = null, EsBloquejat = false }
            };

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public ActionResult<UserDTO> CreateUser([FromBody]UserDTO userDTO)
        {
            if(userDTO == null)
            {
                return BadRequest(userDTO);
            }
            if(userDTO.Id !=Guid.Empty)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            userDTO.Id = new Guid();
            return Ok(userDTO);
        }
    }
}
