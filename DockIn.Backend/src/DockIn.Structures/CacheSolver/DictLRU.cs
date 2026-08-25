using DockIn.Structures;
using DockIn.Structures.interfaces;

public class DictLRU<TKey, TValue> : ICacheResolve<TKey, TValue> where TKey : notnull
{
    private const int DEFAULT_SIZE = 10;
    int _counter;
    int _hits;
    int _misses;

    /// <summary>
    /// Quantidade de vezes que bateu no limite de Cache
    /// Se tiver muitas significa que o melhor é ajustar
    /// </summary>
    int _evictions;

    Dictionary<TKey, LruNode> _map;

    private ILinkedList<LruNode> _usageList;

    public DictLRU()
    {
        _map = new Dictionary<TKey, LruNode>(DEFAULT_SIZE);
        _usageList = new DoubleLinkedList<LruNode>();
        _hits = 0;
        _misses = 0;
        _counter = 0;

    }

    public long Hits => _hits;

    public long Misses => _misses;

    public double HitRatio()
    {
        int total = _hits + _misses;
        if (total == 0)
        {
            return 0.0;
        }

        return _hits / (total * 100.0);
    }

    public long EvictionCount => _evictions;

    public int Count => _counter;

    public int Capacity => _map.Capacity;

    double ICacheResolve<TKey, TValue>.HitRatio => throw new NotImplementedException();

    public void Clear()
    {
        _map = new Dictionary<TKey, LruNode>(DEFAULT_SIZE);
        _usageList = new DoubleLinkedList<LruNode>();

        _hits = 0;
        _misses = 0;
        _counter = 0;
    }

    public bool ContainsKey(TKey key)
    {
        throw new NotImplementedException();
    }

    public bool Remove(TKey key)
    {
        throw new NotImplementedException();
    }

    public void Set(TKey key, TValue value, TimeSpan? expiry = null)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value), "Valor Inválido passado ao Dictionary da Cache");
        }

        if (_map.TryGetValue(key, out LruNode? node))
        {
            _usageList.remove(node);
            _map.Remove(key);
            _counter--;
        }

        if (_counter >= DEFAULT_SIZE)
        {
            LruNode ultimo = _usageList.last();

            _usageList.removeLast();
            _map.Remove(ultimo.Key);

            _evictions++;
            _counter--;
        }

        LruNode novoNode = new LruNode(key, value, expiry);
        _usageList.addFirst(novoNode);
        _map.Add(key, novoNode);

        _counter++;
    }

    public bool TryGet(TKey key, out TValue? value)
    {
        throw new NotImplementedException();
    }

    private class LruNode
    {
        public TKey Key { get; }
        public TValue Value { get; }
        public DateTime? ExpiresAt { get; set; }

        public bool isExpired() => ExpiresAt.HasValue && DateTime.UtcNow > ExpiresAt.Value;

        public LruNode(TKey key, TValue value, TimeSpan? expiry)
        {
            Key = key;
            Value = value;
            ExpiresAt = expiry.HasValue ? DateTime.UtcNow.Add(expiry.Value) : null;
        }

    }
}
