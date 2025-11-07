using PokiMani.Core.Entities;

namespace PokiMani.Core.DTOs
{   public class EnvelopeDto
    {    
        public long? Balance { get; set; }
        public string? Color { get; set; }
        public bool? IsParent { get; set; }
        public string? name { get; set; }
        public Guid? ParentId { get; set; }  
        public DateTime? DateCreated { get; set; }
        public DateTime? DateDestroyed { get; set; }
    }

}
