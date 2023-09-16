

namespace DotnetCoding.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }

        IProductAuditRepository ProductAudits { get; }

        Task<int> Save();
    }
}
