using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Repositories.Bai1.First_Approach
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(int id, bool export = true);
        Task<IEnumerable<T>> GetListAsync();
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> expression, bool export = true);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        int Delete(int id);
        Task<int> MaxIdAsync(int id);
        Task<int> MinIdAsync(int id);
        bool CheckExists(int id);
    }
}
