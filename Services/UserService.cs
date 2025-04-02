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
    }
}
