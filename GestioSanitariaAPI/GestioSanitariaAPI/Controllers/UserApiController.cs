using GestioSanitariaAPI.Models;
using GestioSanitariaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GestioSanitariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        List<UserDTO> userList = new List<UserDTO>
            {
                new UserDTO { Id = new Guid("f6f020cc-edb7-4809-bf60-8f42b3e67932"), UserName = "andreu", Password = "123456", Email = "andreu@example.com", DataAlta = new DateTime(), DataBaixa = null, EsBloquejat = false },
                new UserDTO { Id = new Guid("523e1b90-fad9-41be-9f63-0848d2471b30"), UserName = "anna", Password = "7895", Email = "anna@example.com", DataAlta = new DateTime(), DataBaixa = null, EsBloquejat = false }
            };
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            return Ok(userList);
        }
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> GetUser(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return BadRequest();
            }

            var user = userList.Select(x => x.Id = Id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public ActionResult<UserDTO> CreateUser([FromBody]UserDTO userDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(userDTO == null)
            {
                return BadRequest(userDTO);
            }
            
            userDTO.Id = new Guid();
            userList.AddRange(userDTO);
            return Ok(userDTO);
        }
    }
}
