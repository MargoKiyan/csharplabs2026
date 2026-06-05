using System.Reflection;
using AsyncDataLibrary.Attributes;
using AsyncDataLibrary.Infrastructure;
using AsyncDataLibrary.Interfaces;

namespace AsyncDataLibrary.Repositories;

public class JsonRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly FileStorageProvider _storageProvider;
    private readonly IDataSerializer _serializer;
    private readonly string _fileName;

    public JsonRepository(FileStorageProvider storageProvider, IDataSerializer serializer)
    {
        _storageProvider = storageProvider;
        _serializer = serializer;
        _fileName = GetFileNameFromAttribute();
    }

    public List<T> GetAll()
    {
        string json = _storageProvider.Read(_fileName);
        return _serializer.Deserialize<T>(json);
    }

    public T? GetById(Guid id)
    {
        return GetAll().FirstOrDefault(item => item.Id == id);
    }

    public void Add(T item)
    {
        List<T> items = GetAll();
        items.Add(item);
        Save(items);
    }

    public void Update(T item)
    {
        List<T> items = GetAll();
        int index = items.FindIndex(existingItem => existingItem.Id == item.Id);

        if (index >= 0)
        {
            items[index] = item;
            Save(items);
        }
    }

    public void Delete(Guid id)
    {
        List<T> items = GetAll();
        T? item = items.FirstOrDefault(existingItem => existingItem.Id == id);

        if (item != null)
        {
            items.Remove(item);
            Save(items);
        }
    }

    public async Task<List<T>> GetAllAsync()
    {
        string json = await _storageProvider.ReadAsync(_fileName);
        return await _serializer.DeserializeAsync<T>(json);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        List<T> items = await GetAllAsync();
        return items.FirstOrDefault(item => item.Id == id);
    }

    public async Task AddAsync(T item)
    {
        List<T> items = await GetAllAsync();
        items.Add(item);
        await SaveAsync(items);
    }

    public async Task UpdateAsync(T item)
    {
        List<T> items = await GetAllAsync();
        int index = items.FindIndex(existingItem => existingItem.Id == item.Id);

        if (index >= 0)
        {
            items[index] = item;
            await SaveAsync(items);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        List<T> items = await GetAllAsync();
        T? item = items.FirstOrDefault(existingItem => existingItem.Id == id);

        if (item != null)
        {
            items.Remove(item);
            await SaveAsync(items);
        }
    }

    private void Save(List<T> items)
    {
        string json = _serializer.Serialize(items);
        _storageProvider.Write(_fileName, json);
    }

    private async Task SaveAsync(List<T> items)
    {
        string json = await _serializer.SerializeAsync(items);
        await _storageProvider.WriteAsync(_fileName, json);
    }

    private static string GetFileNameFromAttribute()
    {
        DataFileAttribute? attribute = typeof(T).GetCustomAttribute<DataFileAttribute>();

        if (attribute == null)
        {
            throw new InvalidOperationException($"Для моделі {typeof(T).Name} не вказано DataFileAttribute.");
        }

        return attribute.FileName;
    }
}
