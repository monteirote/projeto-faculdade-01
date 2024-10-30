using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto01.Data;
using Projeto01.Models;
using Projeto01.Services;
using Projeto01.ViewModels;

namespace Projeto01.Controllers {

    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase {

        [HttpPost("login")]
        public IActionResult Login ([FromBody] UserLoginViewModel model, [FromServices] AppDbContext context, [FromServices] TokenService tokenService) {
            var user = context.Users.FirstOrDefault(x => x.Email == model.Email);

            if (user == null)
                return StatusCode(401, new { message = "Usuário ou senha inválidos" });

            if (Settings.GenerateHash(model.Password) != user.Password)
                return StatusCode(401, new { message = "Usuário ou senha inválidos" });

            try
            {
                var token = tokenService.CreateToken(user);
                return Ok(new { token = token });
            }
            catch (Exception e) {
                return StatusCode(500, new { erro = "Falha interna no servidor", exception = e.Message });
            }
        }


        [HttpPost("signup")]
        public IActionResult Signup ([FromBody] UserSignupViewModel model, [FromServices] AppDbContext context)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == model.Email);

            if (user != null)
                return StatusCode(401, new { message = "E-mail já cadastrado" });

            try {

                var userNew = new User {
                    Name = model.Name,
                    Email = model.Email,
                    Password = Settings.GenerateHash(model.Password),
                    Role = "Paciente"
                };

                context.Users.Add(userNew);
                context.SaveChanges();

                return Ok(new { userId = userNew.Id });

            } catch (Exception e) {
                return StatusCode(500, new { erro = "Falha interna no servidor", exception = e.Message });
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get ([FromServices] AppDbContext context) {

            try {
                var users = context.Users.ToList().Select(x => new UserReturnViewModel {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Role = x.Role,
                    Password = x.Password
                });

                return Ok(users);

            } catch (Exception e) {
                return StatusCode(500, new { erro = "Falha interna no servidor", exception = e.Message });
            }
        }
    }
}
