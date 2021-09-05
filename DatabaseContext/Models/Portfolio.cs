
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DatabaseContext.Models
{
    [Table(nameof(Portfolio))]
    public class Portfolio{

        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public IList<PortfolioCoin> Coins { get; set; }

        /*
        [Required]
        public long UsdAmount { get; set; }
        */
    }
}
