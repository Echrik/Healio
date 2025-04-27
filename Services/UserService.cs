using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using Healio.Models;
using Microsoft.EntityFrameworkCore;

namespace Healio.Services
{
    public class UserService
    {
        private readonly HealioDbContext _context;

        public UserService(HealioDbContext context)
        {
            _context = context;
        }

        public bool RegisterUser(User user, string password, string name)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
                return false;

            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                user.PasswordHash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                user.Name = name;
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return true;
        }

        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                Console.WriteLine("User not found via email.. have you registered yet?");
                return null;
            }

            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                if (user.PasswordHash != hashedPassword)
                {
                    Console.WriteLine("Invalid password");
                    return null;
                }
            }

            return user;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }
        public DoctorProfile GetDoctorById(int id)
        {
            return _context.DoctorProfiles.Find(id);
        }
        public DoctorProfile GetDoctorByUserId(int id)
        {
            return _context.DoctorProfiles.Where(d => d.UserId == id).FirstOrDefault();
        }

        public DoctorProfile GetDoctorByUserId(string id)
        {
            return _context.DoctorProfiles.Where(d => d.UserId.ToString() == id).FirstOrDefault();
        }

        public PatientProfile GetPatientById(int id)
        {
            return _context.PatientProfiles.Find(id);
        }

        public PatientProfile GetPatientByUserId(int id)
        {
            return _context.PatientProfiles.Where(p => p.UserId == id).FirstOrDefault();
        }

        public bool RegisterDoctor(DoctorProfile docprof)
        {
            _context.DoctorProfiles.Add(docprof);
            return _context.SaveChanges() > 0;
        }
        public bool RegisterPatient(PatientProfile patprof)
        {
            _context.PatientProfiles.Add(patprof);
            return _context.SaveChanges() > 0;
        }

        internal void UpdatePatientMedicalHistory(int id, string medicalHistory, DateTime dateOfBirth)
        {
            var patient = _context.PatientProfiles.FirstOrDefault(p => p.UserId == id);
            if (patient == null)
            {
                return;
            }
            Console.WriteLine($"{medicalHistory} {dateOfBirth}");
            patient.MedicalHistory = medicalHistory;
            patient.DateOfBirth = dateOfBirth;
            _context.PatientProfiles.Update(patient);
            _context.SaveChanges();
        }

        internal void UpdateDoctorProfile(int id, string clinicAddress, string specialization)
        {
            var doctor = _context.DoctorProfiles.FirstOrDefault(p => p.UserId == id);
            if (doctor == null)
            {
                return;
            }
            Console.WriteLine($"{clinicAddress} {specialization}");
            doctor.ClinicAddress = clinicAddress;
            doctor.Specialization = specialization;
            _context.DoctorProfiles.Update(doctor);
            _context.SaveChanges();
        }

        public bool AddSchedule(DoctorSchedule schedule)
        {
            _context.DoctorSchedules.Add(schedule);
            return _context.SaveChanges() > 0;
        }
        public List<DoctorSchedule> GetSchedulesByDoctorId(int id)
        {
            return _context.DoctorSchedules.Where(s => s.DoctorId == id).ToList();
        }

        public bool DeleteScheduleByDay(string day)
        {
            return _context.DoctorSchedules.Where(s => s.DayOfWeek == day).ExecuteDelete() > 0;
        }
    }
}
