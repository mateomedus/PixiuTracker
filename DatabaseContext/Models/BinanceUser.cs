using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseContext.Models
{
    [Table(nameof(BinanceUser))]
    public class BinanceUser
    {
        /*[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }*/

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(32)]
        public string Username { get; set; }

        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(64)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(256)]
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
    }
}
