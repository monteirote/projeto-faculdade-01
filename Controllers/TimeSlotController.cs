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

                var doctor = await context.Doctors.FirstOrDefault(x => x.Id == timeSlot.doctorId);

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
                return StatusCode(500, new { message = "Falha interna no servidor" });
            }
        }
    }
}
