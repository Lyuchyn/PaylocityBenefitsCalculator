using Api.Dtos.Dependent;

namespace Api.Services
{
    public interface IDependentService
    {
        /// <summary>
        /// Gets all dependents.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>List of dependents or empty list.</returns>
        Task<List<GetDependentDto>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Gets a dependent by id.
        /// </summary>
        /// <param name="id">Dependent id.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The dependent found, or <see langword="null" />.</returns>
        Task<GetDependentDto?> GetById(int id, CancellationToken cancellationToken);
    }
}
