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

        public async Task<List<Employee>> GetAll()
        {
            var employees = await GetBaseQuery().ToListAsync();
            return employees;
        }

        public async Task<Employee?> GetById(int id)
        {
            var employee = await GetBaseQuery().FirstOrDefaultAsync(x => x.Id == id);
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
