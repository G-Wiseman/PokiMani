using PokiMani.Core.Entities;

namespace PokiMani.Core.DTOs
{
    public class CreateAccountTransactionDto
    {
        public required Guid AccountId { get; set; }
        public required int amount { get; set; }
        public string? memo { get; set; }
        public string? notes { get; set; }
        public string? payee { get; set; }
    }

    public class AccountTransactionDto
    {
        public Guid Id { get; set; }
        public Guid? AccountId { get; set; }
        public Account? Account { get; set; }
        public int? amount { get; set; }
        public string? memo { get; set; }
        public string? notes { get; set; }
        public bool? cleared { get; set; }
        public bool? reconciled { get; set; }
        public string? payee { get; set; }
    }
}
