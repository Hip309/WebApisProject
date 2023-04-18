using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApi.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength (100)]
        public string Name { get; set; }
        [Required]
        [MaxLength (100)]
        public string Email { get; set; } = null!;
        [Required]
        [MaxLength (15)]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string Password { get; set; } = null!;
    }
}
