
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DatabaseContext.Models
{
    [Table(nameof(PortfolioCoin))]
    public class PortfolioCoin
    {   
        [Key]
        public int PortfolioId { get; set; }
        [Key]
        public int CoinId { get; set; }

        [ForeignKey(nameof(PortfolioId))]
        public Portfolio Portfolio { get; set; }

        [ForeignKey(nameof(CoinId))]
        public Coin Coin { get; set; }

        [Required]
        public decimal Amount { get; set; }

    }
}
