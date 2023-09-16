using System.ComponentModel.DataAnnotations;

namespace DotnetCoding.Contracts
{
    public class ProductRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        [Required]
        [Range(0, 10000, ErrorMessage = "Price cannot be greater than 10000")]
        public decimal Price { get; set; }

    }
}