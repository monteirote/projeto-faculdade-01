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
                    IsAvailable = true
                };

                await context.TimeSlots.AddAsync(newTimeSlot);
                await context.SaveChangesAsync();

                return Ok(new GetTimeSlotViewModel(newTimeSlot));

            } catch (Exception e) {
                return StatusCode(500, new { erro = "Falha interna no servidor", exception = e.Message });
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin, Paciente")]
        public async Task<IActionResult> GetTimeSlotsBySpecialty ([FromServices] AppDbContext context, [FromQuery] string specialty) {
            try {

                var retorno = new List<GetTimeSlotViewModel>();

                var results = await context.TimeSlots
                                            .Include(x => x.Doctor)
                                            .Where(x => x.Doctor.Specialty == specialty)
                                            .Where(x => x.StartTime >= DateTime.Now)
                                            .ToListAsync();
                                                            
                foreach (var r in results)
                    retorno.Add(new GetTimeSlotViewModel(r));
                

                return Ok(retorno);

            } catch (Exception e)  {
                return StatusCode(500, new { erro = "Falha interna no servidor", exception = e.Message });
            }
        }


        [HttpGet("doctor/{id}")]
        [Authorize(Roles = "Admin, Paciente")]
        public async Task<IActionResult> GetTimeSlotsByDoctor ([FromServices] AppDbContext context, [FromRoute] int id) {
            try {
                var retorno = new List<GetTimeSlotViewModel>();

                var results = await context.TimeSlots
                                                .Include(x => x.Doctor)
                                                .Where(x => x.Doctor.Id == id)
                                                .Where(x => x.StartTime >= DateTime.Now)
                                                .ToListAsync();

                foreach (var r in results)
                    retorno.Add(new GetTimeSlotViewModel(r));

                return Ok(retorno);

            } catch (Exception e) {
                return StatusCode(500, new { erro = "Falha interna no servidor", exception = e.Message });
            }
        }


        [HttpDelete("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> CancelTimeSlot ([FromServices] AppDbContext context, [FromRoute] int id) {
            try {
                var timeSlotFound = (from ts in context.TimeSlots
                                      where ts.Id == id
                                      select ts).FirstOrDefault();

                if (timeSlotFound is null)
                    return NotFound();

                if (!timeSlotFound.IsAvailable)
                    return BadRequest("Não foi possível cancelar esse horário pois já existe uma consulta associada à ele.");

                context.TimeSlots.Remove(timeSlotFound);
                await context.SaveChangesAsync();

                return NoContent();

            } catch (Exception e) { 
                return StatusCode(500, new { erro = "Falha interna no servidor", exception = e.Message });
            }
        }

    }
}
