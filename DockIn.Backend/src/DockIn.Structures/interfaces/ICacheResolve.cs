namespace DockIn.Structures.interfaces;

public interface ICacheResolve<TKey, TValue> where TKey : notnull
{
    // ==========================================
    // 1. OPERAÇÕES CORE DA CACHE
    // ==========================================

    /// <summary>
    /// Tenta obter o valor da chave. Incrementa automaticamente Hits (se encontrar) ou Misses (se falhar/expirar).
    /// </summary>
    bool TryGet(TKey key, out TValue? value);

    /// <summary>
    /// Adiciona ou atualiza um item na cache com um tempo de vida opcional (TTL).
    /// Se a capacidade estiver cheia, despoleta o algoritmo de evicção (LRU / LFU).
    /// </summary>
    void Set(TKey key, TValue value, TimeSpan? expiry = null);

    /// <summary>
    /// Verifica se a chave existe na cache e se ainda não expirou.
    /// </summary>
    bool ContainsKey(TKey key);

    /// <summary>
    /// Remove um item específico da cache.
    /// </summary>
    bool Remove(TKey key);

    /// <summary>
    /// Limpa toda a cache e faz reset aos contadores de telemetria.
    /// </summary>
    void Clear();

    // ==========================================
    // 2. MÉTRICAS DE PERFORMANCE & TELEMETRIA
    // ==========================================

    /// <summary>
    /// Número de vezes que um elemento foi encontrado com sucesso (Cache Hit).
    /// </summary>
    long Hits { get; }

    /// <summary>
    /// Número de vezes que um elemento NÃO foi encontrado ou já tinha expirado (Cache Miss).
    /// </summary>
    long Misses { get; }

    /// <summary>
    /// Percentagem de sucesso da cache (0.0% a 100.0%).
    /// </summary>
    double HitRatio { get; }

    /// <summary>
    /// Número de elementos que foram expulsos/descartados por limite de capacidade.
    /// </summary>
    long EvictionCount { get; }

    // ==========================================
    // 3. CAPACIDADE E ESTADO
    // ==========================================

    /// <summary>
    /// Quantidade de itens guardados atualmente na cache.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Capacidade máxima suportada pela cache antes de aplicar o algoritmo de evicção.
    /// </summary>
    int Capacity { get; }
}
