using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto01.Data;
using Projeto01.Models;
using Projeto01.ViewModels;

namespace Projeto01.Controllers {

    [ApiController]
    [Route("api/timeslot")]
    public class TimeSlotController : ControllerBase {

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTimeSlot ([FromServices] AppDbContext context, [FromBody] CreateTimeSlotViewModel timeSlot) {
            try {

                var doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == timeSlot.DoctorId);

                if (doctor is null)
                    return BadRequest("O médico informado não existe.");

                var newTimeSlot = new TimeSlot {
                    Doctor = doctor,
                    StartTime = timeSlot.StartTime,
                    EndTime = timeSlot.EndTime,
                    IsAvaliable = true
                };

                await context.TimeSlots.AddAsync(newTimeSlot);
                await context.SaveChangesAsync();

                return Ok(newTimeSlot);

            } catch (Exception e) {
                return StatusCode(500, new { erro = "Falha interna no servidor", exception = e.Message });
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin, Paciente")]
        public async Task<IActionResult> GetTimeSlotsBySpecialty ([FromServices] AppDbContext context, [FromQuery] string specialty) {
            try {
                var results = await context.TimeSlots
                                            .Include(x => x.Doctor)
                                            .Where(x => x.Doctor.Specialty == specialty)
                                            .Where(x => x.StartTime >= DateTime.Now)
                                            .ToListAsync();

                return Ok(results);

            } catch (Exception e)  {
                return StatusCode(500, new { erro = "Falha interna no servidor", exception = e.Message });
            }
        }
    }
}
