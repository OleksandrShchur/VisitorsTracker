using System.Threading.Tasks;

namespace VisitorsTracker.Db.IBaseService
{
    public interface IBaseService<T>
        where T : class
    {
        Task<T> Insert(T entity);

        Task<T> Update(T entity);

        Task<T> Delete(T entity);
    }
}
