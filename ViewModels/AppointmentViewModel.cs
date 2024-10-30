using System.ComponentModel.DataAnnotations;
using Projeto01.Models;

namespace Projeto01.ViewModels
{
    public class CreateAppointmentViewModel
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int TimeSlotId { get; set; }

        [Required]
        public int PatientId { get; set; }

        public string Notes { get; set; } = string.Empty;
    }

    public class GetAppointmentViewModel {
        public int Id { get; set; }
        public GetDoctorViewModel Doctor { get; set; } = new GetDoctorViewModel();
        public User Patient { get; set; } = new User();
        public DateTime CreatedAt { get; set; }
        public SimpleTimeSlotViewModel TimeSlot { get; set; } = new SimpleTimeSlotViewModel();
        public string Notes { get; set; } = string.Empty;

        public GetAppointmentViewModel (Appointment appointment) {
            Id = appointment.Id;
            Doctor = new GetDoctorViewModel { Id = appointment.Doctor.Id, Name = appointment.Doctor.Name, Specialty = appointment.Doctor.Specialty };
            Patient = appointment.Patient;
            CreatedAt = appointment.CreatedAt;
            TimeSlot = new SimpleTimeSlotViewModel { Id = appointment.TimeSlot.Id, StartTime = appointment.TimeSlot.StartTime, EndTime = appointment.TimeSlot.EndTime, IsAvailable = appointment.TimeSlot.IsAvailable  };
            Notes = appointment.Notes;
        }
    }
}
