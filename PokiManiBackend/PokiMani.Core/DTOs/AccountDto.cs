namespace PokiMani.Core.DTOs
{
    public class AccountDto
    {
        public string? name { get; set; }

        public DateTime? createdDate { get; set; }

        public DateTime? closedDate { get; set; }

        public long? balance { get; set; }
    }
}
