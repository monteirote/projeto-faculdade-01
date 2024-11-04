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
    [Route("api/appointment")]
    public class AppointmentController : ControllerBase {

        [HttpPost]
        [Authorize (Roles = "Admin, Paciente")]
        public async Task<IActionResult> CreateAppointment ([FromServices] AppDbContext context, [FromBody] CreateAppointmentViewModel model) {
            try {

                var doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == model.DoctorId);
                if (doctor is null)
                    return BadRequest("O DoctorID enviado não corresponde à nenhum registro.");


                var patient = await context.Users.FirstOrDefaultAsync(x => x.Email == model.PatientEmail);
                if (patient is null)
                    return BadRequest("O PatientID enviado não corresponde à nenhum registro.");


                var timeslot = await context.TimeSlots.FirstOrDefaultAsync(x => x.Id == model.TimeSlotId);
                if (timeslot is null || !timeslot.IsAvailable)
                    return BadRequest("O TimeSlotID enviado não corresponde à nenhum registro ou já possui uma consulta marcada.");

                var newAppointment = new Appointment {
                    Doctor = doctor,
                    Patient = patient,
                    TimeSlot = timeslot,
                    CreatedAt = DateTime.Now,
                    Notes = model.Notes
                };

                await context.Appointments.AddAsync(newAppointment);

                timeslot.IsAvailable = false;

                await context.SaveChangesAsync();

                return Ok (new GetAppointmentViewModel(newAppointment));
            } catch (Exception e) {
                return StatusCode(500, new { erro = "Falha interna no servidor", exception = e.Message });
            }
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Paciente")]
        public async Task<IActionResult> GetAppointment ([FromServices] AppDbContext context, [FromRoute] int id) { 
            try {
                var appointment = await context.Appointments.Include(x => x.Doctor)
                                                                .Include(x => x.Patient)
                                                                .Include(x => x.TimeSlot)
                                                                .FirstOrDefaultAsync(x => x.Id == id);

                if (appointment is null)
                    return NotFound();

                return Ok(new GetAppointmentViewModel(appointment));

            } catch (Exception e) {
                return StatusCode(500, new { erro = "Falha interna no servidor", exception = e.Message });
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Paciente")]
        public async Task<IActionResult> DeleteAppointment ([FromServices] AppDbContext context, [FromRoute] int id) { 
            try {
                var appointmentFound = await context.Appointments.FirstOrDefaultAsync(x => x.Id == id);

                if (appointmentFound is null)
                    return NotFound();

                if (appointmentFound.TimeSlot.StartTime > DateTime.Now)
                    return BadRequest("Não é possível cancelar uma consulta com o horário de início maior que o horário atual.");

                appointmentFound.TimeSlot.IsAvailable = true;

                context.Appointments.Remove(appointmentFound);
                await context.SaveChangesAsync();

                return NoContent();

            } catch (Exception e) {
                return StatusCode(500, new { erro = "Falha interna no servidor", exception = e.Message });
            }
        }


        [HttpGet("patient/{id}")]
        [Authorize(Roles = "Admin, Paciente")]
        public async Task<IActionResult> GetAppointmentsByUser ([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var patient = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (patient is null)
                return BadRequest("O PatientID enviado não corresponde à nenhum registro.");

            var results = await context.Appointments.Where(x => x.Patient.Id == id).ToListAsync();

            return Ok(results);
        }
    }
}
