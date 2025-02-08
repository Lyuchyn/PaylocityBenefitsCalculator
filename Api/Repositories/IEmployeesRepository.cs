using Api.Models;

namespace Api.Repositories
{
    public interface IEmployeesRepository
    {
        Task<List<Employee>> GetAll();

        Task<Employee?> GetById(int id);
    }
}
