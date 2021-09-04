using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseContext.Models
{
    [Table(nameof(Coin))]
    public class Coin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        public long Price { get; set; }



    }
}
