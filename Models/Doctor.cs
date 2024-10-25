using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto01.Models
{
    public class Doctor {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public List<TimeSlot> Availability { get; set; } = new List<TimeSlot>();
    }
}
