using System.Drawing;

namespace PokiMani.Models.Entities
{
    public class Bucket
    {
        public Guid Id { get; set; }

        public virtual required User User { get; set; }

        public virtual Bucket? Parent { get; set; }

        public required bool isParent { get; set; }

        public DateTime? dateDestroyed { get; set; }

        public required DateTime dateCreated { get; set; }

        public required long balance { get; set; }

        public string? color { get; set; }
    }
}
