using DotnetCoding.Core.Interfaces;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductDbContext _dbContext;
        public IProductRepository Products { get; }

        public IProductAuditRepository ProductAudits { get; }

        public UnitOfWork(ProductDbContext dbContext,
                            IProductRepository productRepository, IProductAuditRepository productAuditRepository)
        {
            _dbContext = dbContext;
            Products = productRepository;
            ProductAudits = productAuditRepository;
        }

        public Task<int> Save()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

    }
}
