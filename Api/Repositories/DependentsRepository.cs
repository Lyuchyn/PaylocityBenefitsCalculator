using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class DependentsRepository : IDependentsRepository
    {
        private readonly AppDbContext _dbContext;

        public DependentsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Dependent>> GetAll(CancellationToken cancellationToken = default)
        {
            var dependents = await _dbContext.Dependents
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return dependents;
        }

        public async Task<Dependent?> GetById(int id, CancellationToken cancellationToken = default)
        {
            var dependent = await _dbContext.Dependents
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return dependent;
        }
    }
}
