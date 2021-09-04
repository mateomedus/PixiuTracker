
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DatabaseContext.Models
{
    [Table(nameof(Portfolio))]
    public class Portfolio{

        
        [Key]
        public int Id { get; set; }

        [Required]            
        public int CoinID { get; set; }

        [Required]
        public long Amount { get; set; }

        /*
        [Required]
        public long UsdAmount { get; set; }
        */
    }
}
