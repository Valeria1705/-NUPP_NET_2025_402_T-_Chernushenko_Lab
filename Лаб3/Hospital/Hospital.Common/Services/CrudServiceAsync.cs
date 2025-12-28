using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hospital.Common.Services
{
    public interface ICrudServiceAsync<T> : IEnumerable<T>
    {
        Task<bool> CreateAsync(T element);
        Task<T> ReadAsync(Guid id);
        Task<IEnumerable<T>> ReadAllAsync();
        Task<IEnumerable<T>> ReadAllAsync(int page, int amount);
        Task<bool> UpdateAsync(T element);
        Task<bool> RemoveAsync(T element);
        Task<bool> SaveAsync();
    }

    public class CrudServiceAsync<T> : ICrudServiceAsync<T> where T : class
    {
        private readonly ConcurrentDictionary<Guid, T> _storage = new();
        private readonly string _filePath;

        public CrudServiceAsync(string filePath)
        {
            _filePath = filePath;
        }

        private Guid GetId(T element)
        {
            var prop = element.GetType().GetProperty("Id");
            if (prop == null) throw new InvalidOperationException("Type does not have an Id property");
            var val = prop.GetValue(element);
            if (val is Guid g) return g;
            throw new InvalidOperationException("Id property is not a Guid");
        }

        public async Task<bool> CreateAsync(T element)
        {
            var id = GetId(element);
            if (!_storage.TryAdd(id, element)) return false;
            await SaveAsync();
            return true;
        }

        public Task<T> ReadAsync(Guid id)
        {
            if (_storage.TryGetValue(id, out var element))
                return Task.FromResult(element);
            throw new KeyNotFoundException("Element not found");
        }

        public Task<IEnumerable<T>> ReadAllAsync() => Task.FromResult<IEnumerable<T>>(_storage.Values.ToList());

        public Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            if (page < 1 || amount < 1) throw new ArgumentException("Page and amount must be >= 1");
            var items = _storage.Values.Skip((page - 1) * amount).Take(amount).ToList();
            return Task.FromResult<IEnumerable<T>>(items);
        }

        public async Task<bool> UpdateAsync(T element)
        {
            var id = GetId(element);
            if (!_storage.ContainsKey(id)) return false;
            _storage[id] = element;
            await SaveAsync();
            return true;
        }

        public async Task<bool> RemoveAsync(T element)
        {
            var id = GetId(element);
            var result = _storage.TryRemove(id, out _);
            if (result) await SaveAsync();
            return result;
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                };
                var json = JsonSerializer.Serialize(_storage.Values.ToList(), options);
                await File.WriteAllTextAsync(_filePath, json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerator<T> GetEnumerator() => _storage.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}