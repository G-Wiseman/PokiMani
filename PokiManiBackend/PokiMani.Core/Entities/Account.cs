using PokiMani.Core.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokiMani.Core.Entities
{
    public class Account : IUserOwned
    {
        public Guid Id { get; set; }

        public required Guid UserId { get; set; }

        public required string name { get; set; }

        public required DateTime createdDate { get; set; }
        
        public DateTime? closedDate { get; set; }


        public long balance {  get; set; }
    }
}
