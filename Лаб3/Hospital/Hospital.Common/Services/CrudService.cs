using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hospital.Common.Services
{
    public delegate void EntityChangedHandler<T>(object sender, T entity);

    public class CrudService<T> : ICrudService<T> where T : class
    {
        protected readonly Dictionary<Guid, T> _storage = new Dictionary<Guid, T>();

        public event EntityChangedHandler<T>? OnCreated;
        public event EntityChangedHandler<T>? OnUpdated;
        public event EntityChangedHandler<T>? OnRemoved;

        protected Guid GetId(T element)
        {
            var prop = element.GetType().GetProperty("Id");
            if (prop == null) throw new InvalidOperationException("Type does not have an Id property");
            var val = prop.GetValue(element);
            if (val is Guid g) return g;
            throw new InvalidOperationException("Id property is not a Guid");
        }
        public virtual void Create(T element)
        {
            var id = GetId(element);
            if (_storage.ContainsKey(id)) throw new InvalidOperationException("Element already exists");
            _storage[id] = element;
            OnCreated?.Invoke(this, element);
        }
        public virtual T Read(Guid id)
        {
            if (_storage.TryGetValue(id, out var element))
                return element;
            throw new KeyNotFoundException("Element not found");
        }
        public virtual IEnumerable<T> ReadAll() => _storage.Values.ToList();
        public virtual void Update(T element)
        {
            var id = GetId(element);
            if (!_storage.ContainsKey(id)) throw new KeyNotFoundException("Element not found");
            _storage[id] = element;
            OnUpdated?.Invoke(this, element);
        }
        public virtual void Remove(T element)
        {
            var id = GetId(element);
            if (_storage.Remove(id))
            {
                OnRemoved?.Invoke(this, element);
            }
        }
        public virtual void Save(string filePath)
        {
            var items = ReadAll();
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            var json = JsonSerializer.Serialize(items, options);
            File.WriteAllText(filePath, json);
        }
        public virtual void Load(string filePath)
        {
            if (!File.Exists(filePath)) return;
            var json = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var items = JsonSerializer.Deserialize<List<T>>(json, options);
            _storage.Clear();
            if (items == null) return;
            foreach (var it in items)
            {
                try
                {
                    var id = GetId(it);
                    _storage[id] = it;
                }
                catch
                {
                }
            }
        }
    }
}