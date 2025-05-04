using Healio.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Healio.Services
{
    public class AppointmentService
    {
        private readonly HealioDbContext _context;

        public AppointmentService(HealioDbContext context)
        {
            _context = context;
        }

        public List<User> GetDoctors()
        {
            return _context.Users.Where(d => d.Role == "doctor").ToList();
        }

        public List<TimeSpan> GetAvailableTimes(int doctorId, string day)
        {
            List<TimeSpan> availableTimes = new List<TimeSpan>();
            var docId = _context.DoctorProfiles.Where(d => d.UserId == doctorId).FirstOrDefault().Id;
            DoctorSchedule schedule = _context.DoctorSchedules.Where(d => d.DoctorId == docId && d.DayOfWeek == day).FirstOrDefault();

            if (schedule == null)
            {
                return new List<TimeSpan>();
            }

            for (var time = schedule.StartTime; time < schedule.EndTime; time = time.Add(TimeSpan.FromMinutes(30)))
            {
                availableTimes.Add(time);
            }

            return availableTimes;
        }

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
