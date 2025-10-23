using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokiMani.Models.Entities
{
    public class Account
    {
        public Guid Id { get; set; }

        public virtual required User User { get; set; }

        public string? name { get; set; }



        public DateTime? createdDate { get; set; }
        
        public DateTime? closedDate { get; set; }


        public long balance {  get; set; }

        public long startingBalance { get; set; }
    }
}
