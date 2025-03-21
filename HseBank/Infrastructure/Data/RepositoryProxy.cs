using System.Collections.Concurrent;
using HseBank.Domain.Interfaces;

namespace HseBank.Infrastructure.Data;

public class RepositoryProxy<T> : IRepository<T> where T : IEntity
{
    private readonly IRepository<T> _repository; // Основной репозиторий (работает с БД)
    private readonly ConcurrentDictionary<int, T> _cache; // In-memory кэш

    public RepositoryProxy(IRepository<T> repository)
    {
        _repository = repository;
        _cache = new ConcurrentDictionary<int, T>();

        // При старте загружаем все данные в кэш
        foreach (var entity in _repository.GetAll())
        {
            _cache[entity.Id] = entity;
        }
    }

    public void Add(T entity)
    {
        _repository.Add(entity); // Записываем в БД
        _cache[entity.Id] = entity; // Кладём в кэш
    }

    public T Get(int id)
    {
        // Пробуем найти в кэше
        if (_cache.TryGetValue(id, out var entity))
        {
            return entity;
        }

        // Если нет — загружаем из БД и кладём в кэш
        entity = _repository.Get(id);
        if (entity != null)
        {
            _cache[id] = entity;
        }

        return entity;
    }

    public void Update(T entity)
    {
        _repository.Update(entity); // Обновляем в БД
        _cache[entity.Id] = entity; // Обновляем в кэше
    }

    public void Delete(int id)
    {
        _repository.Delete(id); // Удаляем из БД
        _cache.TryRemove(id, out _); // Удаляем из кэша
    }

    public IEnumerable<T> GetAll()
    {
        return _cache.Values; // Просто отдаём все данные из кэша
    }
}