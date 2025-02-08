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

        public async Task<List<Dependent>> GetAll()
        {
            var dependents = await _dbContext.Dependents
                .AsNoTracking()
                .ToListAsync();

            return dependents;
        }

        public async Task<Dependent?> GetById(int id)
        {
            var dependent = await _dbContext.Dependents
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return dependent;
        }
    }
}
