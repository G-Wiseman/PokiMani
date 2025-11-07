namespace PokiMani.Core.DTOs
{
    public class AddUserDto
    {

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }
        public string? Phone { get; set; }


    }
}
