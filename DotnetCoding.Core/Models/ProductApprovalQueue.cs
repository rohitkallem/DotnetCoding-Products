namespace DotnetCoding.Core.Models
{
    public class ProductApprovalQueue
    {
        public string Name { get; set; } = null!;
        public int ProductState { get; set; }
        public int RequestStatus { get; set; }
        public int? RequestReason { get; set; } = null;
        public DateTime? RequestDate { get; set; } = null;
    }
}
