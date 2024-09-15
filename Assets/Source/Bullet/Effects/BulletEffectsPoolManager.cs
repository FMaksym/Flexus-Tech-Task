using UnityEngine;

public class BulletEffectsPoolManager : MonoBehaviour
{
    [Header("Decal Settings")]
    [SerializeField] private Decal _decalPrefab;
    [SerializeField] private Transform _decalPoolParent;
    [SerializeField] private int _decalPoolSize = 10;

    [Header("Explosion Settings")]
    [SerializeField] private Explosion _explosionPrefab;
    [SerializeField] private Transform _explosionPoolParent;
    [SerializeField] private int _explosionPoolSize = 5;

    private ObjectPool<Decal> _decalPool;
    private ObjectPool<Explosion> _explosionPool;

    private void Awake()
    {
        InitializePools();
    }

    private void InitializePools()// Method for initializing object pools
    {
        _decalPool = new ObjectPool<Decal>(_decalPrefab, _decalPoolSize, _decalPoolParent);
        _explosionPool = new ObjectPool<Explosion>(_explosionPrefab, _explosionPoolSize, _explosionPoolParent);
    }

    // Getter for obtaining a pool of traces (Decal)
    public ObjectPool<Decal> GetDecalPool()
    {
        return _decalPool;
    }

    // Getter for getting Explosion pool (Explosion)
    public ObjectPool<Explosion> GetExplosionPool()
    {
        return _explosionPool;
    }
}