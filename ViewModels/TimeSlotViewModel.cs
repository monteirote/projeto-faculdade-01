using System.ComponentModel.DataAnnotations;
using Projeto01.Models;

namespace Projeto01.ViewModels {
    public class CreateTimeSlotViewModel {

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }

    public class GetTimeSlotViewModel {
        public int Id { get; set; }
        public GetDoctorViewModel Doctor { get; set; } = new GetDoctorViewModel();
        public DateTime StartTime { get; set; } = new DateTime();
        public DateTime EndTime { get; set; } = new DateTime();
        public bool IsAvailable { get; set; } = false;

        public GetTimeSlotViewModel (TimeSlot timeSlot) {
            Id = timeSlot.Id;
            Doctor = new GetDoctorViewModel { Name = timeSlot.Doctor.Name, Specialty = timeSlot.Doctor.Specialty };
            StartTime = timeSlot.StartTime;
            EndTime = timeSlot.EndTime;
            IsAvailable = timeSlot.IsAvailable;
        }
    }

    public class SimpleTimeSlotViewModel {
        public int Id { get; set; }
        public DateTime StartTime { get; set; } = new DateTime();
        public DateTime EndTime { get; set; } = new DateTime();
        public bool IsAvailable { get; set; } = false;
    }
}
