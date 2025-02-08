using Api.Dtos.Dependent;

namespace Api.Services.Dependents
{
    public interface IDependentService
    {
        /// <summary>
        /// Gets all dependents.
        /// </summary>
        /// <returns>List of dependents or empty list.</returns>
        Task<List<GetDependentDto>> GetAll();

        /// <summary>
        /// Gets a dependent by id.
        /// </summary>
        /// <param name="id">Dependent id.</param>
        /// <returns>The dependent found, or <see langword="null" />.</returns>
        Task<GetDependentDto?> GetById(int id);
    }
}
