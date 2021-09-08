using Microsoft.OData.Edm;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DatabaseContext.Models
{
    public class CoinHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Date { get; set; }

    }
}
