namespace AsyncDataLibrary.Interfaces;

public interface IRepository<T> where T : class, IEntity
{
    List<T> GetAll();
    T? GetById(Guid id);
    void Add(T item);
    void Update(T item);
    void Delete(Guid id);

    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T item);
    Task UpdateAsync(T item);
    Task DeleteAsync(Guid id);
}
