namespace Healio.Models.DTO
{
    public class SendMessageDTO
    {
        public int ReceiverId { get; set; }  
        public string Content { get; set; } = string.Empty;
    }
}
