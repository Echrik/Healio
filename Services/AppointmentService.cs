using Healio.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

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

        public List<Appointment> GetReservationsForDoctorAndPatient(int doctorId, int patientId)
        {
            var docId = _context.DoctorProfiles.Where(d => d.UserId == doctorId).FirstOrDefault().Id;
            var patId = _context.PatientProfiles.Where(d => d.UserId == patientId).FirstOrDefault().Id;

            return _context.Appointments
                .Where(a => a.DoctorId == docId && a.PatientId == patId && a.Status == "pending")
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToList();
        }
        public List<PatientProfile> GetPatientsForDoctor(int doctorId)
        {
            var docId = _context.DoctorProfiles.Where(d => d.UserId == doctorId).FirstOrDefault().Id;
            return _context.Appointments
                .Where(a => a.DoctorId == docId)
                .Include(a => a.Patient.User)
                .Select(a => a.Patient)
                .Distinct()
                .ToList();
        }
        public bool BookAppointment(Appointment appointment)
        {
            if (_context.Appointments.Contains(appointment))
                return false;
            _context.Appointments.Add(appointment);
            return _context.SaveChanges() > 0;
        }
        public Appointment GetAppointmentById(int id)
        {
            return _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefault(a => a.Id == id);
        }
        public bool CloseAppointment(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null)
                return false;
            appointment.Status = "closed";
            return _context.SaveChanges() > 0;
        }
    }
}
