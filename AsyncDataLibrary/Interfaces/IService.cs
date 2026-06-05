namespace AsyncDataLibrary.Interfaces;

public interface IService<T> where T : class, IEntity
{
    void Add(T item);
    Task AddAsync(T item);
    List<T> GetAll();
    Task<List<T>> GetAllAsync();
    T? GetById(Guid id);
    Task<T?> GetByIdAsync(Guid id);
    void Update(T item);
    Task UpdateAsync(T item);
    void Delete(Guid id);
    Task DeleteAsync(Guid id);
}
