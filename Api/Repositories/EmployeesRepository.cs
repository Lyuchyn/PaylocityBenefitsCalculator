using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly AppDbContext _dbContext;

        public EmployeesRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> GetAll(CancellationToken cancellationToken = default)
        {
            var employees = await GetBaseQuery().ToListAsync(cancellationToken);
            return employees;
        }

        public async Task<Employee?> GetById(int id, CancellationToken cancellationToken = default)
        {
            var employee = await GetBaseQuery().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return employee;
        }

        private IQueryable<Employee> GetBaseQuery()
        {
            return _dbContext.Employees
                .AsNoTracking()
                .Include(x => x.Dependents);
        }
    }
}
