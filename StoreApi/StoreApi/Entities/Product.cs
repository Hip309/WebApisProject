
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApi.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string Unit { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double Cost { get; set;}
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set;}
        public string? Image { get; set; }

        //Foreign Key
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
    }
}
