using PokiMani.Core.Entities;

namespace PokiMani.Core.DTOs
{
    public class CreateEnvelopeTransactionDto
    {
        public Guid? EnvelopeId { get; set; }
        public Guid? AccountTransactionId { get; set; }
        public int amount { get; set; }
    }


    public class EnvelopeTransactionDto
    {
        public Guid? EnvelopeId { get; set; }
        public Guid? AccountTransactionId { get; set; }
        public int? amount { get; set; }
    }
}
