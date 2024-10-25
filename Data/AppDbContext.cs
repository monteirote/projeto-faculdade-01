using Microsoft.EntityFrameworkCore;
using Projeto01.Models;

namespace Projeto01.Data {
    public class AppDbContext : DbContext {

        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder options) {
            options.UseSqlite("Data Source=app.db; Cache=Shared");
        }
    }
}
