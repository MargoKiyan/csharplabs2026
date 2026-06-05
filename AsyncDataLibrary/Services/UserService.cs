using AsyncDataLibrary.Interfaces;
using AsyncDataLibrary.Models;

namespace AsyncDataLibrary.Services;

public class UserService : IService<User>
{
    private readonly IRepository<User> _repository;

    public UserService(IRepository<User> repository)
    {
        _repository = repository;
    }

    public void Add(User item)
    {
        PrepareUser(item);
        _repository.Add(item);
    }

    public async Task AddAsync(User item)
    {
        PrepareUser(item);
        await _repository.AddAsync(item);
    }

    public List<User> GetAll()
    {
        return _repository.GetAll();
    }

    public Task<List<User>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public User? GetById(Guid id)
    {
        return _repository.GetById(id);
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        return _repository.GetByIdAsync(id);
    }

    public void Update(User item)
    {
        _repository.Update(item);
    }

    public Task UpdateAsync(User item)
    {
        return _repository.UpdateAsync(item);
    }

    public void Delete(Guid id)
    {
        _repository.Delete(id);
    }

    public Task DeleteAsync(Guid id)
    {
        return _repository.DeleteAsync(id);
    }

    private static void PrepareUser(User user)
    {
        if (user.Id == Guid.Empty)
        {
            user.Id = Guid.NewGuid();
        }

        if (user.RegisteredAt == default)
        {
            user.RegisteredAt = DateTime.Now;
        }
    }
}
