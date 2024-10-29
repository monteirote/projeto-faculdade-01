using System.ComponentModel.DataAnnotations;

namespace Projeto01.ViewModels {
    public class CreateTimeSlotViewModel {

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }
}
