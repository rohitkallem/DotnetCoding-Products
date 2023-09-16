namespace DotnetCoding.Contracts
{
    public class ProductApprovalQueue
    {
        public string Name { get; set; } = null!;
        public int ProductState { get; set; }
        public int RequestStatus { get; set; }
        public int? RequestReason { get; set; }
        public DateTime? RequestDate { get; set; }
    }
}
