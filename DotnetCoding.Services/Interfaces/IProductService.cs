using DotnetCoding.Contracts;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Core.Models.ProductResponse> PostProduct(ProductRequest request);
        Task<Core.Models.ProductResponse> PutProduct(int productId, ProductRequest request);
        Task DeleteProduct(int ID);
        Task<IEnumerable<Core.Models.ProductApprovalQueue>> GetProductApprovalQueue();

        Task ReviewProductRequest(Core.Models.ProductReviewRequest request);
        Task<IEnumerable<Core.Models.ProductResponse>> SearchProducts(Core.Models.SearchRequest request);
    }
}
