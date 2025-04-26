namespace Healio.Models.DTO
{
    public class PublicUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public void Deconstruct(out string name, out string email, out string role)
        {
            name = Name;
            email = Email;
            role = Role;
        }
    }
}
