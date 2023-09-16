
namespace DotnetCoding.Core.Models
{
    public enum ProductState
    {
        Create, Update, Delete
    }

    public enum RequestStatus
    {
        Pending, Approved, Rejected
    }

    public enum RequestReason
    {
        PriceGreaterThanLimit, PriceGreaterThanHalfPreviousValue, Deleted
    }

    public class ProductAudit
    {
        public int ProductAuditID { get; set; }
        public int ProductID { get; set; }
        public ProductState ProductState { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public RequestReason? RequestReason { get; set; }
        public DateTime? RequestDate { get; set; }

    }
}
