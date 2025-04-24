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

        public async Task<bool> RegisterUserAsync(User user, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                return false;

            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                user.PasswordHash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

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
        public PatientProfile GetPatientById(int id)
        {
            return _context.PatientProfiles.Find(id);
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
    }
}
