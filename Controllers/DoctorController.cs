using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto01.Data;
using Projeto01.Models;
using Projeto01.ViewModels;

namespace Projeto01.Controllers {

    [ApiController]
    [Route("api/doctor")]
    public class DoctorController : ControllerBase {

        [HttpGet]
        [Authorize(Roles = "Admin", "Paciente")]
        public async Task<IActionResult> GetAllDoctorsAsync ([FromServices] AppDbContext context) {
            try {
                var doctors = await context.Doctors.ToListAsync();
                return Ok(doctors);

            } catch (Exception e) {
                return StatusCode(500, new { erro = "Falha interna no servidor", exception = e.Message });
            }
        }


        [HttpGet("{id}")]
        [Authorize (Roles = "Admin", "Paciente")]
        public async Task<IActionResult> GetDoctorById ([FromServices] AppDbContext context, [FromRoute] int id) {
            try {
                var doctorFound = await context.Doctors.FindAsync(id);

                if (doctorFound is null)
                    return NotFound();

                return Ok(doctorFound);

            } catch (Exception e) {
                return StatusCode(500, new { erro = "Falha interna no servidor", exception = e.Message });
            }
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDoctor ([FromBody] CreateDoctorViewModel doctor, [FromServices] AppDbContext context) {
            try {

                var newDoctor = new Doctor {
                    Name = doctor.Name,
                    Specialty = doctor.Specialty,
                    ProfilePicture = doctor.ProfilePicture
                };

                await context.Doctors.AddAsync(newDoctor);
                await context.SaveChangesAsync();

                return Ok();

            } catch (Exception e) {
                return StatusCode(500, new { erro = "Falha interna no servidor", exception = e.Message });
            }
        }
    }
}
