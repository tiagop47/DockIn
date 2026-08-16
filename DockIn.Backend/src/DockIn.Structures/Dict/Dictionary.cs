using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace DockIn.Structures;

public class MyDict<TChave, TValor> : IDictionary<TChave, TValor>
{
    private const int ARRAY_INIT = 16;

    /// <summary>
    /// Fator de Carga é o número a = itens / número buckets
    /// </summary>
    private const float LOADFACTOR_TRESHOLD = 0.75f;

    private ILinkedList<MyKeyValuePair<TChave, TValor>>[] _bucket;
    private int _counter;
    private int _version;

    public MyDict()
    {
        _bucket = new DoubleLinkedList<MyKeyValuePair<TChave, TValor>>[ARRAY_INIT];
        _counter = 0;
        _version = 0;
    }

    public TValor this[TChave key]
    {
        get
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "A chave não pode ser nula.");
            }

            int indice = GetBucketIndex(key, _bucket.Length);

            if (_bucket[indice] != null)
            {
                foreach (var item in _bucket[indice])
                {
                    if (EqualityComparer<TChave>.Default.Equals(item.Key, key))
                    {
                        return item.Value;
                    }
                }
            }

            throw new KeyNotFoundException($"A chave '{key}' não foi encontrada no dicionário.");
        }
        set
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "A chave não pode ser nula.");
            }

            int indice = GetBucketIndex(key, _bucket.Length);

            if (_bucket[indice] == null)
            {
                _bucket[indice] = new DoubleLinkedList<MyKeyValuePair<TChave, TValor>>();
            }
            else
            {
                foreach (var item in _bucket[indice])
                {
                    if (EqualityComparer<TChave>.Default.Equals(item.Key, key))
                    {
                        _bucket[indice].remove(item);
                        _bucket[indice].addFirst(new MyKeyValuePair<TChave, TValor>(key, value));
                        _version++;
                        return;
                    }
                }
            }

            _bucket[indice].addFirst(new MyKeyValuePair<TChave, TValor>(key, value));
            _version++;
            _counter++;

            if (LoadFactorAbused())
            {
                Resize();
            }
        }
    }

    public ICollection<TChave> Keys
    {
        get
        {
            ICollection<TChave> tmp = new List<TChave>();

            foreach (var pares in _bucket)
            {
                if (pares == null) continue;

                foreach (var item in pares)
                {
                    tmp.Add(item.Key);
                }
            }

            return tmp;
        }
    }

    public ICollection<TValor> Values
    {
        get
        {
            ICollection<TValor> tmp = new List<TValor>();

            foreach (var pares in _bucket)
            {
                if (pares == null) continue;

                foreach (var item in pares)
                {
                    tmp.Add(item.Value);
                }
            }

            return tmp;
        }
    }

    public int Count => _counter;

    public bool IsReadOnly => false;

    public void Add(TChave key, TValor value)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key), "A chave não pode ser nula.");
        }

        int indice = GetBucketIndex(key, _bucket.Length);

        if (_bucket[indice] == null)
        {
            _bucket[indice] = new DoubleLinkedList<MyKeyValuePair<TChave, TValor>>();
        }
        else
        {
            foreach (var element in _bucket[indice])
            {
                if (EqualityComparer<TChave>.Default.Equals(element.Key, key))
                {
                    throw new ArgumentException($"A chave '{key}' já existe no dicionário.");
                }
            }
        }

        MyKeyValuePair<TChave, TValor> chaveValor = new MyKeyValuePair<TChave, TValor>(key, value);
        _bucket[indice].addFirst(chaveValor);

        _counter++;
        _version++;

        if (LoadFactorAbused())
        {
            Resize();
        }
    }

    private void Resize()
    {
        int novoTamanho = _bucket.Length * 2;
        var novosBuckets = new DoubleLinkedList<MyKeyValuePair<TChave, TValor>>[novoTamanho];

        foreach (var bucketAntigo in _bucket)
        {
            if (bucketAntigo == null) continue;

            foreach (var element in bucketAntigo)
            {
                int novoIndice = GetBucketIndex(element.Key, novoTamanho);

                if (novosBuckets[novoIndice] == null)
                {
                    novosBuckets[novoIndice] = new DoubleLinkedList<MyKeyValuePair<TChave, TValor>>();
                }

                novosBuckets[novoIndice].addFirst(element);
            }
        }

        _bucket = novosBuckets;
    }

    public void Add(KeyValuePair<TChave, TValor> item)
    {
        Add(item.Key, item.Value);
    }

    public void Clear()
    {
        _bucket = new DoubleLinkedList<MyKeyValuePair<TChave, TValor>>[ARRAY_INIT];
        _counter = 0;
        _version++;
    }

    public bool Contains(KeyValuePair<TChave, TValor> item)
    {
        if (item.Key == null) return false;

        int indice = GetBucketIndex(item.Key, _bucket.Length);

        if (_bucket[indice] == null) return false;

        foreach (var objeto in _bucket[indice])
        {
            bool mesmaChave = EqualityComparer<TChave>.Default.Equals(objeto.Key, item.Key);
            bool mesmoValor = EqualityComparer<TValor>.Default.Equals(objeto.Value, item.Value);

            if (mesmaChave && mesmoValor)
            {
                return true;
            }
        }

        return false;
    }

    public bool ContainsKey(TChave key)
    {
        if (key == null) return false;

        int indice = GetBucketIndex(key, _bucket.Length);

        if (_bucket[indice] == null) return false;

        foreach (var objeto in _bucket[indice])
        {
            if (EqualityComparer<TChave>.Default.Equals(objeto.Key, key))
            {
                return true;
            }
        }

        return false;
    }

    public void CopyTo(KeyValuePair<TChave, TValor>[] array, int arrayIndex)
    {
        if (array == null) throw new ArgumentNullException(nameof(array));

        if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));

        if (array.Length - arrayIndex < _counter)
            throw new ArgumentException("O array de destino não tem espaço suficiente.");

        int indiceAtual = arrayIndex;

        foreach (var bucket in _bucket)
        {
            if (bucket == null) continue;

            foreach (var item in bucket)
            {
                array[indiceAtual] = new KeyValuePair<TChave, TValor>(item.Key, item.Value);
                indiceAtual++;
            }
        }
    }

    public IEnumerator<KeyValuePair<TChave, TValor>> GetEnumerator()
    {
        int versaoInicial = _version;

        foreach (var bucket in _bucket)
        {
            if (bucket == null) continue;

            foreach (var item in bucket)
            {
                if (versaoInicial != _version)
                {
                    throw new InvalidOperationException("A coleção foi modificada durante a iteração.");
                }

                yield return new KeyValuePair<TChave, TValor>(item.Key, item.Value);
            }
        }
    }

    public bool Remove(TChave key)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key), "A chave não pode ser nula.");
        }

        int indice = GetBucketIndex(key, _bucket.Length);

        if (_bucket[indice] == null) return false;

        foreach (var item in _bucket[indice])
        {
            if (EqualityComparer<TChave>.Default.Equals(item.Key, key))
            {
                _bucket[indice].remove(item);
                _counter--;
                _version++;
                return true;
            }
        }

        return false;
    }

    public bool Remove(KeyValuePair<TChave, TValor> item)
    {
        if (item.Key == null) return false;

        int indice = GetBucketIndex(item.Key, _bucket.Length);

        if (_bucket[indice] == null) return false;

        foreach (var objeto in _bucket[indice])
        {
            bool mesmaChave = EqualityComparer<TChave>.Default.Equals(objeto.Key, item.Key);
            bool mesmoValor = EqualityComparer<TValor>.Default.Equals(objeto.Value, item.Value);

            if (mesmaChave && mesmoValor)
            {
                _bucket[indice].remove(objeto);
                _counter--;
                _version++;
                return true;
            }
        }

        return false;
    }

    public bool TryGetValue(TChave key, [MaybeNullWhen(false)] out TValor value)
    {
        if (key == null)
        {
            value = default;
            return false;
        }

        int indice = GetBucketIndex(key, _bucket.Length);

        if (_bucket[indice] != null)
        {
            foreach (var item in _bucket[indice])
            {
                if (EqualityComparer<TChave>.Default.Equals(item.Key, key))
                {
                    value = item.Value;
                    return true;
                }
            }
        }

        value = default;
        return false;
    }

    private bool LoadFactorAbused()
    {
        return (float)_counter / _bucket.Length > LOADFACTOR_TRESHOLD;
    }

    private uint HashFunction(TChave chave)
    {
        if (chave is null) return 0;
        return (uint)EqualityComparer<TChave>.Default.GetHashCode(chave);
    }

    private int GetBucketIndex(TChave chave, int capacidade)
    {
        return (int)(HashFunction(chave) % capacidade);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private struct MyKeyValuePair<TKey, TValue>
    {
        public TKey Key { get; }
        public TValue Value { get; }

        public MyKeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
