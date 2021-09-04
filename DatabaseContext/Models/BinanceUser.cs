using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DatabaseContext.Models
{
    [Table(nameof(BinanceUser))]
    public class BinanceUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        [JsonIgnore]
        public string Password { get; set; }

        [Required]
        [MaxLength(128)]
        public string Email { get; set; }

        [Required]
        [MaxLength(256)]
        public string ApiKey { get; set; }

        [Required]
        [MaxLength(256)]
        public string ApiSecret { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PorfolioID { get; set; }
    }
}
