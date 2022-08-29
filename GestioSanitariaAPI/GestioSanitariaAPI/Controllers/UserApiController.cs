using GestioSanitariaAPI.Data;
using GestioSanitariaAPI.Models;
using GestioSanitariaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestioSanitariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        private string secretKey;

        public UserApiController(ApplicationDbContext db, IConfiguration _configuration)
        {
            _db = db;
            secretKey = _configuration.GetValue<string>("ApiSettings:Secret");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            var usersList = _db.Users.ToList();
            return Ok(usersList);
        }

        [HttpPost()]
        [Route("LoginUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> LoginUser([FromForm] string userName, [FromForm] string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                // Cambiar para mostrar un mensaje
                return BadRequest();
            }

            var user = _db.Users.FirstOrDefault(u => u.UserName == userName && u.Password == password);

            if (user == null)
            {
                return NotFound();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userDTO = new UserDTO
            {
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                DataAlta = user.DataAlta,
                DataBaixa = user.DataBaixa,
                EsBloquejat = user.EsBloquejat,
                Token = tokenHandler.WriteToken(token)
            };

            return Ok(userDTO);
        }

        [HttpPost]
        public ActionResult<UserDTO> CreateUser([FromForm] UserCreateDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (userDTO == null)
            {
                return BadRequest(userDTO);
            }

            User model = new()
            {
                UserName = userDTO.UserName,
                Password = userDTO.Password,
                Email = userDTO.Email,
                DataAlta = userDTO.DataAlta,
                DataBaixa = userDTO.DataBaixa,
                EsBloquejat = userDTO.EsBloquejat,
                Rol = userDTO.Rol
            };

            _db.Users.Add(model);
            _db.SaveChanges();
            return Ok(model);
        }
    }
}
