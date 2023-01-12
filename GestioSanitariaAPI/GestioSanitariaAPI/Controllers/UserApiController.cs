using GestioSanitariaAPI.Data;
using GestioSanitariaAPI.Models;
using GestioSanitariaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        //private readonly HttpContextAccessor _httpContextAccessor;

        Guid userId = Guid.Empty;

        public UserApiController(ApplicationDbContext db, IConfiguration _configuration)
        {
            _db = db;
            secretKey = _configuration.GetValue<string>("JWT:Secret");

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            var usersList = (from u in _db.Users
                             join r in _db.Roles
                             on u.Rol equals r.Id
                             select new
                             {
                                 UserName = u.UserName,
                                 Password = u.Password,
                                 Email = u.Email,
                                 DataAlta = u.DataAlta,
                                 DataBaixa = u.DataBaixa,
                                 EsBloquejat = u.EsBloquejat,
                                 Rol = r.Rol
                             }).ToList();

            return Ok(usersList);
        }

        [HttpPost()]
        [Route("LoginUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> LoginUser([FromForm] string userName, [FromForm] string password)
        {
            string nameUser = "";
            string userRol = "";
            Boolean esBloquejat = false;
            DateTime dataAlta = DateTime.MinValue;
            DateTime? dataBaixa = DateTime.MinValue;
            string pass = "";
            string email = "";
            List<string> claimList = new List<string>();
            JwtSecurityToken token = new JwtSecurityToken();

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                // Cambiar para mostrar un mensaje
                return BadRequest();
            }

            var user = (from u in _db.Users
                        join r in _db.Roles
                        on u.Rol equals r.Id
                        where u.UserName == userName && u.Password == password
                        select new
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            Password = u.Password,
                            Email = u.Email,
                            DataAlta = u.DataAlta,
                            DataBaixa = u.DataBaixa,
                            EsBloquejat = u.EsBloquejat,
                            Rol = r.Rol
                        }).ToList();

            foreach (var u in user)
            {
                userId = u.Id;
                nameUser = u.UserName;
                userRol = u.Rol;
                esBloquejat = u.EsBloquejat;
                dataAlta = u.DataAlta;
                dataBaixa = u.DataBaixa;
                pass = u.Password;
                email = u.Email;

            }

            if (user == null)
            {
                return NotFound();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new[]
            {
                    new Claim(ClaimTypes.Sid, userId.ToString()),
                    new Claim(ClaimTypes.Name, nameUser),
                    new Claim(ClaimTypes.Role, userRol)
            };

                        
            token = new JwtSecurityToken(claims: claims,
                                         expires: DateTime.UtcNow.AddDays(1),
                                         signingCredentials: new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));

            var userDTO = new UserDTO
            {
                UserName = nameUser,
                Password = pass,
                Email = email,
                DataAlta = dataAlta,
                DataBaixa = dataBaixa,
                EsBloquejat = esBloquejat,
                Rol = userRol,
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
                Rol = userDTO.Rol
            };

            _db.Users.Add(model);
            _db.SaveChanges();
            return Ok(model);
        }
    }
}
