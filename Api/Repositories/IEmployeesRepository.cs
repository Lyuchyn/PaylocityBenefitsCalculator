using Api.Models;

namespace Api.Repositories
{
    public interface IEmployeesRepository
    {
        Task<List<Employee>> GetAll(CancellationToken cancellationToken);

        Task<Employee?> GetById(int id, CancellationToken cancellationToken);
    }
}
