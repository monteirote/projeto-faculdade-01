using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto01.Models {
    public class Appointment {
        public int Id { get; set; }
        public Doctor Doctor { get; set; } = new Doctor();
        public User Patient { get; set; } = new User();
        public DateTime AppointmentDate { get; set; }
        public TimeSlot TimeSlot { get; set; } = new TimeSlot();
        public string Notes { get; set; } = string.Empty;
    }
}
