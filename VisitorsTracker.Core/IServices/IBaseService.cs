namespace VisitorsTracker.Db.IBaseService
{
    public interface IBaseService<T>
        where T : class
    {
        T Insert(T entity);

        T Update(T entity);

        T Delete(T entity);
    }
}
