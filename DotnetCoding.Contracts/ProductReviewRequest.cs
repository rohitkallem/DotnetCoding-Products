using DotnetCoding.Core.Models;

namespace DotnetCoding.Contracts
{
    public class ProductReviewRequest
    {
        public int ReviewRequestId { get; set; }
        public int ProductId { get; set; }
        public RequestStatus Status { get; set; }
    }
}
