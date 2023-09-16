using DotnetCoding.Core.Models;


namespace DotnetCoding.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<ProductApprovalQueue>> GetProductApprovalQueue();

        Task<ProductAudit> GetProductReviewRequest(int reviewRequestId, int productID);

        Task<IEnumerable<Product>> SearchProducts(SearchRequest request);
    }
}
