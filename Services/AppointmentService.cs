using Healio.Models;
using Microsoft.EntityFrameworkCore;

namespace Healio.Services
{
    public class AppointmentService
    {
        private readonly HealioDbContext _context;

        public AppointmentService(HealioDbContext context)
        {
            _context = context;
        }

        public async Task<List<DoctorProfile>> GetDoctorsAsync()
        {
            return await _context.DoctorProfiles.ToListAsync();
        }

        //public async Task<List<DateTime>> GetAvailableBookingTimesAsync(int doctorId, DateTime date)
        //{
        //    // Example logic: Fetch available times for a doctor on a specific date
        //    var doctorSchedule = await _context.DoctorSchedules
        //        .FirstOrDefaultAsync(ds => ds.DoctorId == doctorId && ds.Date == date);

        //    if (doctorSchedule == null)
        //    {
        //        return new List<DateTime>(); // No schedule found
        //    }

        //    // Return available times (assuming DoctorSchedule has a collection of available slots)
        //    return doctorSchedule.AvailableTimes;
        //}

        //public async Task<bool> BookAppointmentAsync(Appointment appointment)
        //{
        //    // Check if the time slot is still available
        //    var existingAppointment = await _context.Appointments
        //        .FirstOrDefaultAsync(a => a.DoctorId == appointment.DoctorId &&
        //                                  a.Date == appointment.Date &&
        //                                  a.Time == appointment.Time);

        //    if (existingAppointment != null)
        //    {
        //        return false; // Time slot already booked
        //    }

        //    _context.Appointments.Add(appointment);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}
    }
}
