using Microsoft.EntityFrameworkCore;
namespace Healio.Models
{
    public class HealioDbContext : DbContext
    {
        public HealioDbContext(DbContextOptions<HealioDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<PatientProfile> PatientProfiles { get; set; }
        public DbSet<DoctorProfile> DoctorProfiles { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<HealthData> HealthData { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            modelBuilder.Entity<DoctorSchedule>()
                .Property(d => d.DayOfWeek)
                .HasConversion<string>();

            modelBuilder.Entity<Appointment>()
                .Property(a => a.Status)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Appointment>()
                .HasIndex(a => a.PatientId);

            modelBuilder.Entity<Appointment>()
                .HasIndex(a => a.DoctorId);
        }
    }
}
