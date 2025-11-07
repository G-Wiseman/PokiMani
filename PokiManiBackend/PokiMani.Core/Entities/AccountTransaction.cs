using PokiMani.Core.Interfaces.IRepositories;

namespace PokiMani.Core.Entities
{
    public class AccountTransaction : IUserOwned
    {
        public Guid Id { get; set; }

        public required Guid UserId { get; set; }
        public required Guid AccountId { get; set; }
        public Account? Account { get; set; }   
        public required int amount { get; set; }
        public string? memo { get; set; }
        public string? notes { get; set; }
        public required bool cleared { get; set; }
        public required bool reconciled { get; set; }
        public string? payee { get; set; }
    }
}
