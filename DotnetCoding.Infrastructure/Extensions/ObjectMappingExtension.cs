namespace DotnetCoding.Infrastructure.Extensions
{
    public static class ObjectMappingExtension
    {
        public static IEnumerable<Contracts.ProductApprovalQueue> ToContracts(this IEnumerable<Core.Models.ProductApprovalQueue> queue)
        {
            return queue?.Select(q => q?.ToContract()).ToList() ?? new List<Contracts.ProductApprovalQueue>();
        }

        public static Contracts.ProductApprovalQueue ToContract(this Core.Models.ProductApprovalQueue productApprovalQueue)
        {
            return new Contracts.ProductApprovalQueue()
            {
                Name = productApprovalQueue.Name,
                ProductState = productApprovalQueue.ProductState,
                RequestStatus = productApprovalQueue.RequestStatus,
                RequestReason = productApprovalQueue.RequestReason,
                RequestDate = productApprovalQueue.RequestDate
            };
        }

        public static Core.Models.SearchRequest ToDomain(this Contracts.SearchRequest searchRequest)
        {
            return new Core.Models.SearchRequest()
            {
                Name = searchRequest.Name,
                StartDate = searchRequest.StartDate,
                EndDate = searchRequest.EndDate,
                MinPrice = searchRequest.MinPrice,
                MaxPrice = searchRequest.MaxPrice
            };
        }

        public static IEnumerable<Core.Models.ProductResponse> AsProductResponses(this IEnumerable<Core.Models.Product> products)
        {
            return products?.Select(q => q?.AsProductResponse()).ToList() ?? new List<Core.Models.ProductResponse>();
        }

        public static Core.Models.ProductResponse AsProductResponse(this Core.Models.Product product)
        {
            return new Core.Models.ProductResponse()
            {
                ID = product.ID,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedDate = product.CreatedDate,
                ModifiedDate = product.ModifiedDate
            };

        }

        public static Core.Models.ProductReviewRequest AsProductReviewRequest(this Contracts.ProductReviewRequest productReviewRequest)
        {
            return new Core.Models.ProductReviewRequest()
            {
                ReviewRequestId = productReviewRequest.ReviewRequestId,
                ProductId = productReviewRequest.ProductId,
                Status = productReviewRequest.Status
            };
        }
    }
}
