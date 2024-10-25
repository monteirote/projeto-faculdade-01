using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto01.Data;
using Projeto01.Models;
using Projeto01.Services;
using Projeto01.ViewModels;

namespace Projeto01.Controllers {

    [ApiController]
    [Route("api/doctor")]
    public class DoctorController : ControllerBase {

        public async IActionResult CreateDoctor ([FromBody] CreateDoctorViewModel doctor, [FromServices] AppDbContext context) {
            try {

            } catch (Exception e) {
                return StatusCode(500, new { erro = "Falha interna no servidor", exception = e.Message });
            }
        }
    }
}
