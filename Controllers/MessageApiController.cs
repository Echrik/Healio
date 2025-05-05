using Healio.Models.DTO;
using Healio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[Route("api/messages")]
[ApiController]
public class MessageApiController : ControllerBase
{
    private readonly MessageService _service;

    public MessageApiController(MessageService service)
    {
        _service = service;
    }

    [HttpGet("partners")]
    public async Task<IActionResult> GetChatPartners()
    {
        var partners = await _service.GetChatPartnersAsync();
        return Ok(partners.Select(p => new { p.Id, p.Name }));
    }

    [HttpGet]
    public async Task<IActionResult> GetMessages([FromQuery] int userId)
    {
        var currentUserId = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var uid) ? uid : 0;
        var messages = await _service.GetMessagesAsync(userId);

        return Ok(messages.Select(m => new
        {
            m.Id,
            m.Content,
            m.SentAt,
            SenderName = m.Sender.Name,
            isMine = m.SenderId == currentUserId
        }));
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Content)) return BadRequest("Content is required.");
        await _service.SendMessageAsync(dto);
        return Ok(new { success = true });
    }
}
