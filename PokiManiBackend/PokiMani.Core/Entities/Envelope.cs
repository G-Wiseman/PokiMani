using PokiMani.Core.Interfaces.IRepositories;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace PokiMani.Core.Entities
{
    public class Envelope : IUserOwned
    { 
        public Guid Id { get; set; }
        public required Guid UserId { get; set; }

        public Guid? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public Envelope? Parent { get; set; }

        public string? name { get; set; }

        public required bool isParent { get; set; }

        public DateTime? dateDestroyed { get; set; }

        public required DateTime dateCreated { get; set; }

        public required long balance { get; set; }

        public string? color { get; set; }
    }
}
