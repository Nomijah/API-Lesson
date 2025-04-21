using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;

namespace Lektion_SUT24_250414_API_intro.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<ICollection<T>> GetAsync();
        public Task<T?> GetByIdAsync(int id);
        public Task CreateAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
    }
}
