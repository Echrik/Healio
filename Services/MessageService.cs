using Healio.Models;
using Healio.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class MessageService
{
    private readonly HealioDbContext _context;
    private readonly IHttpContextAccessor _http;

    public MessageService(HealioDbContext context, IHttpContextAccessor http)
    {
        _context = context;
        _http = http;
    }

    private int GetCurrentUserId()
    {
        var userIdStr = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(userIdStr, out var id) ? id : throw new Exception("User not authenticated.");
    }

    public async Task<List<User>> GetChatPartnersAsync()
    {
        var currentUserId = GetCurrentUserId();
        var currentUser = await _context.Users.FindAsync(currentUserId);

        if (currentUser == null) throw new Exception("User not found.");

        if (currentUser.Role == "patient")
        {
            // Páciensek az összes orvost látják
            return await _context.Users
                .Where(u => u.Role == "doctor")
                .ToListAsync();
        }

        if (currentUser.Role == "doctor")
        {
            // Orvos csak azokat a pácienseket látja, akikkel már beszélt
            var patientIds = await _context.Messages
                .Where(m => m.SenderId == currentUserId || m.ReceiverId == currentUserId)
                .Select(m => m.Sender.Role == "patient" ? m.SenderId : m.ReceiverId)
                .Distinct()
                .ToListAsync();

            return await _context.Users
                .Where(u => patientIds.Contains(u.Id) && u.Role == "patient")
                .ToListAsync();
        }

        return new List<User>();
    }

    public async Task<List<Message>> GetMessagesAsync(int otherUserId)
    {
        var currentUserId = GetCurrentUserId();

        return await _context.Messages
            .Where(m =>
                (m.SenderId == currentUserId && m.ReceiverId == otherUserId) ||
                (m.SenderId == otherUserId && m.ReceiverId == currentUserId))
            .Include(m => m.Sender)
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }

    public async Task SendMessageAsync(SendMessageDTO dto)
    {
        var senderId = GetCurrentUserId();

        var message = new Message
        {
            SenderId = senderId,
            ReceiverId = dto.ReceiverId,
            Content = dto.Content,
            SentAt = DateTime.UtcNow
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
    }
}
