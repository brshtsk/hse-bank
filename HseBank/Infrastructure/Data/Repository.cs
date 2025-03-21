using HseBank.Domain.Interfaces;
using System.Collections.Concurrent;

namespace HseBank.Infrastructure.Data;

public class Repository<T> : IRepository<T> where T : IEntity
{
    private readonly ConcurrentDictionary<int, T> _storage = new();

    public void Add(T entity)
    {
        _storage[entity.Id] = entity;
    }

    public T Get(int id)
    {
        _storage.TryGetValue(id, out var entity);
        return entity;
    }

    public void Update(T entity)
    {
        if (!_storage.ContainsKey(entity.Id))
            throw new ArgumentException("Entity not found");

        _storage[entity.Id] = entity;
    }

    public void Delete(int id)
    {
        _storage.TryRemove(id, out _);
    }

    public IEnumerable<T> GetAll()
    {
        return _storage.Values;
    }
}