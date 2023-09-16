

namespace DotnetCoding.Core.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
        public ICollection<ProductAudit> ProductAudits { get; set; }
      
    }
}
