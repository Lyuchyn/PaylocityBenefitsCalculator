using Api.Models;

namespace Api.Repositories
{
    public interface IDependentsRepository
    {
        Task<List<Dependent>> GetAll();

        Task<Dependent?> GetById(int id);
    }
}
