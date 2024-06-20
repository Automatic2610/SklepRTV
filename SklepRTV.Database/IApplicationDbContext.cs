using Microsoft.EntityFrameworkCore;

namespace SklepRTV.Model
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken =
       default);
    }
}