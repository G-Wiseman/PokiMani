using PokiMani.Core.Interfaces.IRepositories;

namespace PokiMani.Core.Entities
{
    public class EnvelopeTransaction : IUserOwned
    {
        public Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public Guid? EnvelopeId { get; set; }
        public Envelope? Envelope { get; set; }
        public Guid? AccountTransactionId { get; set;}
        public AccountTransaction? AccountTransaction { get; set; }
        public required int amount { get; set; }
    }
}
