using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ProductDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<ProductApprovalQueue>> GetProductApprovalQueue()
        {
            var query = _dbContext.Products.Join(_dbContext.ProductAudits, product => product.ID, productaudit => productaudit.ProductID, (product, productaudit) => new ProductApprovalQueue
            {
                Name = product.Name,
                ProductState = (int)productaudit.ProductState,
                RequestStatus = (int)productaudit.RequestStatus,
                RequestReason = (int?)productaudit.RequestReason,
                RequestDate = (DateTime?)productaudit.RequestDate

            }).Where(p => p.RequestStatus == (int)RequestStatus.Pending);

            var productQueue = await query.ToListAsync().ConfigureAwait(false);

            return productQueue;
        }

        public async Task<ProductAudit> GetProductReviewRequest(int reviewRequestId, int productID)
        {
            var productReviewResponse = await _dbContext.ProductAudits.FirstOrDefaultAsync(pa => pa.ProductID == productID && pa.ProductAuditID == reviewRequestId);

            return productReviewResponse;

        }

        public async Task<IEnumerable<Product>> SearchProducts(SearchRequest request)
        {
            var query = _dbContext.Products.Join(_dbContext.ProductAudits, product => product.ID, productaudit => productaudit.ProductID, (p, pa) => new { product = p, productaudit = pa }).Where(o => o.productaudit.RequestStatus == RequestStatus.Approved).Select(o => o.product);

            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.Name))
                    query = query.Where(p => p.Name.ToLower() == request.Name.ToLower());
                if (request.StartDate.HasValue)
                    query = query.Where(p => p.CreatedDate >= request.StartDate);
                if (request.EndDate.HasValue)
                    query = query.Where(p => p.CreatedDate <= request.EndDate);
                if (request.MinPrice > 0)
                    query = query.Where(p => p.Price >= request.MinPrice);
                if (request.MaxPrice > 0)
                    query = query.Where(p => p.Price <= request.MaxPrice);
                
            }

            var searchResults = await query.OrderByDescending(o => o.ModifiedDate).ToListAsync().ConfigureAwait(false);

            return searchResults;
        }
    }
}
