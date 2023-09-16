namespace DotnetCoding.Core.Models
{
    public class ProductReviewRequest
    {
        public int ReviewRequestId { get; set; }
        public int ProductId { get; set; }
        public RequestStatus Status { get; set; }
    }
}
