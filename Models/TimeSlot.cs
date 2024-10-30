namespace Projeto01.Models {
    public class TimeSlot {
        public int Id { get; set; }
        public Doctor Doctor { get; set; } = new Doctor(); 
        public DateTime StartTime { get; set; } = new DateTime();
        public DateTime EndTime { get; set; } = new DateTime();
        public bool IsAvailable { get; set; } = false;
    }
}
