using Api.Models;

namespace Api.Repositories
{
    public interface IDependentsRepository
    {
        Task<List<Dependent>> GetAll(CancellationToken cancellationToken);

        Task<Dependent?> GetById(int id, CancellationToken cancellationToken);
    }
}
