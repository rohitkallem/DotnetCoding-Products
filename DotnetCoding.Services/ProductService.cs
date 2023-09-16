using DotnetCoding.Contracts;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;
using DotnetCoding.Infrastructure.Extensions;

namespace DotnetCoding.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var productList = await _unitOfWork.Products.GetAll();
            return productList.OrderByDescending(o => o.CreatedDate);
        }

        public async Task<Product> GetProductById(int id)
        {
            var productDetail = await _unitOfWork.Products.GetById(id);
            return productDetail;
        }

        public async Task<Core.Models.ProductResponse> PostProduct(ProductRequest request)
        {
            var product = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                CreatedDate = DateTime.Now,
                Price = request.Price,
                ProductAudits = new List<ProductAudit>()
                {
                    new ProductAudit { ProductState = (int)ProductState.Create }
                }
            };

            ProcessRequestStatus(product);

            _unitOfWork.Products.Insert(product);    
            await _unitOfWork.Save();

            return new Core.Models.ProductResponse()
            {
                Name = product.Name,
                Description = product.Description,
                CreatedDate = product.CreatedDate,
                Price = product.Price,
                ID = product.ID
            };

        }

        public async Task<Core.Models.ProductResponse> PutProduct(int productId, ProductRequest request)
        {
            var existingProduct = await _unitOfWork.Products.GetById(productId);

            var product = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                ModifiedDate = DateTime.Now,
                Price = request.Price,
                ProductAudits = new List<ProductAudit>()
                {
                    new ProductAudit { ProductState = ProductState.Update }
                }
            };

            ProcessRequestStatus(product, existingProduct);

            _unitOfWork.Products.Update(product);
            await _unitOfWork.Save();

            return new Core.Models.ProductResponse()
            {
                Name = product.Name,
                Description = product.Description,
                CreatedDate = product.CreatedDate,
                ModifiedDate = product.ModifiedDate,
                Price = product.Price,
                ID = product.ID
            };

        }

        public async Task DeleteProduct(int Id)
        {
            var existingProduct = await _unitOfWork.Products.GetById(Id);

            existingProduct.ModifiedDate = DateTime.UtcNow;
            existingProduct.DeletedDate = DateTime.UtcNow;
            existingProduct.ProductAudits = new List<ProductAudit>()
            {
                new ProductAudit { ProductState = ProductState.Delete, RequestStatus = RequestStatus.Pending, RequestReason = RequestReason.Deleted, RequestDate = DateTime.Now }
            };

            _unitOfWork.Products.Update(existingProduct);
            await _unitOfWork.Save();

            return;
        }

        public async Task<IEnumerable<Core.Models.ProductApprovalQueue>> GetProductApprovalQueue()
        {
            var productApprovalQueue = await _unitOfWork.Products.GetProductApprovalQueue();

            return productApprovalQueue;
        }

        public async Task ReviewProductRequest(Core.Models.ProductReviewRequest request)
        {
            var existingProduct = await _unitOfWork.Products.GetById(request.ProductId);

            var productReviewRequest = await _unitOfWork.Products.GetProductReviewRequest(request.ReviewRequestId, request.ProductId);

            productReviewRequest.RequestStatus = request.Status;

            existingProduct.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.Products.Update(existingProduct);
            _unitOfWork.ProductAudits.Update(productReviewRequest);
            await _unitOfWork.Save();

            return;
        }

        public async Task<IEnumerable<Core.Models.ProductResponse>> SearchProducts(Core.Models.SearchRequest request)
        {
            var productsList = await _unitOfWork.Products.SearchProducts(request);

            return productsList.AsProductResponses();
        }


        private void ProcessRequestStatus(Product newValues, Product? oldValues = null)
        {
            var newProductDetails = newValues.ProductAudits.FirstOrDefault();

            if (newValues.Price > 5000)
            {
                newProductDetails.RequestStatus = RequestStatus.Pending;
                newProductDetails.RequestReason = RequestReason.PriceGreaterThanLimit;

            }
            else if (oldValues != null)
            {
                var diffPercentage = ((newValues.Price - oldValues.Price) / oldValues.Price) * 100;

                if (diffPercentage > 50)
                {
                    newProductDetails.RequestStatus = RequestStatus.Pending;
                    newProductDetails.RequestReason = RequestReason.PriceGreaterThanHalfPreviousValue;
                }
            }
            else
            {
                newProductDetails.RequestStatus = RequestStatus.Approved;
            }

        }


    }
}
