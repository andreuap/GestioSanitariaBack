using Fluent.Infrastructure.FluentModel;
using GestioSanitariaAPI.Data;
using GestioSanitariaAPI.Models;
using GestioSanitariaAPI.Models.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GestioSanitariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
       
        
        Guid userId = Guid.Empty;
        public PacientesAPIController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PacientesCreateDTO>> GetUsers()
        {
            var usersList = (from u in _db.Users
                             join r in _db.Roles
                             on u.Rol equals r.Id
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

            return Ok(usersList);
        }

        [HttpPost]
        public async ActionResult<PacientesCreateDTO> CreateFichaPaciente([FromForm] PacientesCreateDTO pacientesCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (pacientesCreateDTO == null)
            {
                return BadRequest(pacientesCreateDTO);
            }

            var user = await GetCurrentUserAsync();

            userId = user?.Id;


            Paciente model = new()
            {
                Nom = pacientesCreateDTO.Nom,
                PrimerCognom = pacientesCreateDTO.PrimerCognom,
                SegonCognom = pacientesCreateDTO.SegonCognom,
                Email = pacientesCreateDTO.Email,
                Edat = pacientesCreateDTO.Edat,
                DataNeixament = pacientesCreateDTO.DataNeixament,
                Genere = pacientesCreateDTO.Genere,
                Domicili = pacientesCreateDTO.Domicili,
                Telefon = pacientesCreateDTO.Telefon,
                CreatedDate = DateTime.Now,
                CreateUserId = userId,
                UpdatedDate = DateTime.Now,
                UpdateUserID = userId,
               
            };

            _db.Pacientes.Add(model);
            _db.SaveChanges();
            return Ok(model);
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
