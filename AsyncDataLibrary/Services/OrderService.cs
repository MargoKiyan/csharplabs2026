using AsyncDataLibrary.Interfaces;
using AsyncDataLibrary.Models;

namespace AsyncDataLibrary.Services;

public class OrderService : IService<Order>
{
    private readonly IRepository<Order> _repository;

    public OrderService(IRepository<Order> repository)
    {
        _repository = repository;
    }

    public void Add(Order item)
    {
        PrepareOrder(item);
        _repository.Add(item);
    }

    public async Task AddAsync(Order item)
    {
        PrepareOrder(item);
        await _repository.AddAsync(item);
    }

    public List<Order> GetAll()
    {
        return _repository.GetAll();
    }

    public Task<List<Order>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Order? GetById(Guid id)
    {
        return _repository.GetById(id);
    }

    public Task<Order?> GetByIdAsync(Guid id)
    {
        return _repository.GetByIdAsync(id);
    }

    public List<Order> GetOrdersByUserId(Guid userId)
    {
        return _repository.GetAll().Where(order => order.UserId == userId).ToList();
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(Guid userId)
    {
        List<Order> orders = await _repository.GetAllAsync();
        return orders.Where(order => order.UserId == userId).ToList();
    }

    public void Update(Order item)
    {
        _repository.Update(item);
    }

    public Task UpdateAsync(Order item)
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

    private static void PrepareOrder(Order order)
    {
        if (order.Id == Guid.Empty)
        {
            order.Id = Guid.NewGuid();
        }

        if (order.OrderDate == default)
        {
            order.OrderDate = DateTime.Now;
        }

        if (string.IsNullOrWhiteSpace(order.Status))
        {
            order.Status = "Created";
        }
    }
}
