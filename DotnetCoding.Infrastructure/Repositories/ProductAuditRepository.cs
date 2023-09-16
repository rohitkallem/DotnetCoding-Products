using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ProductAuditRepository : GenericRepository<ProductAudit>, IProductAuditRepository
    {
        public ProductAuditRepository(ProductDbContext dbContext) : base(dbContext)
        {

        }
    }
}
