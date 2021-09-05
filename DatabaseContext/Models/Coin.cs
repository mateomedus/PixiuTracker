using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseContext.Models
{
    [Table(nameof(Coin))]
    public class Coin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public IList<PortfolioCoin> Portfolios { get; set; }

    }
}
