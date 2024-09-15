using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private int _poolSize = 5;
    
    [Header("ShootPower Parametres")]
    [SerializeField] private float _currentShootPower = 25f;
    [SerializeField] private float _minShootPower = 1f;
    [SerializeField] private float _maxShootPower = 25f;

    [Space(10)]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    
    private Queue<GameObject> _projectilePool;

    [Inject] private BulletEffectsPoolManager _bulletEffectsPoolManager;

    private void OnEnable()
    {
        UIModel.OnFirePowerChanged += ChangeFirePower; // Subscribe to the shot power change event
    }

    private void Awake()
    {
        InitializePool();
    }

    // Initialization of the projectile pool
    private void InitializePool()
    {
        _currentShootPower = _maxShootPower; // Set the initial power of the shot to the maximum value

        _projectilePool = new Queue<GameObject>(); // Initialization of the pool queue

        for (int i = 0; i < _poolSize; i++) 
        {
            CreateBullet(); // Creating projectiles for the pool
        }
    }

    // Create a new projectile
    private void CreateBullet()
    {
        GameObject projectile = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation, transform);
        projectile.GetComponent<BulletMovement>().Initialize(this, _currentShootPower, _bulletEffectsPoolManager);

        projectile.SetActive(false);
        _projectilePool.Enqueue(projectile);
    }

    // Method for getting a projectile from the pool and activating it
    public void GetBullet()
    {
        if (_projectilePool.Count > 0) 
        {
            GameObject projectile = _projectilePool.Dequeue(); // Get the projectile from the pool
            projectile.transform.position = _firePoint.position;
            projectile.transform.rotation = _firePoint.rotation;
            projectile.SetActive(true); // Activate the projectile
        }
        else
        {
            CreateBullet(); // If the pool is empty, create a new projectile
            return;
        }
    }

    // Method for returning a projectile to the pool after use
    public void ReturnToPool(GameObject projectile)
    {
        projectile.SetActive(false); // Deactivate the projectile
        projectile.transform.position = _firePoint.position; // Return it to its original position

        // Rebuild the projectile mesh
        BulletMeshGenerator meshGenerator = projectile.GetComponent<BulletMeshGenerator>();
        if (meshGenerator != null)
        {
            meshGenerator.GenerateMesh(); // Generate a new projectile mesh
        }

        _projectilePool.Enqueue(projectile); // Return the projectile to the pool
    }

    // Method for changing the power of the shot
    private void ChangeFirePower(float firePower)
    {
        _currentShootPower = firePower;
        UpdateAllBulletsFirePower(); // Update the power of all inactive projectiles
    }
    
    private void UpdateAllBulletsFirePower()
    {
        foreach (GameObject projectile in _projectilePool) // Update the power of all bullets in the bullet
        {
            if (projectile.activeSelf) continue; // Skip active projectiles

            BulletMovement bulletMovement = projectile.GetComponent<BulletMovement>(); 
            if (bulletMovement != null)
            {
                bulletMovement.UpdateFirePower(_currentShootPower); // Update the power of an inactive projectile
            }
        }
    }

    // Getting the maximum shot power  
    public float GetMaxShootPower()
    {
        return _maxShootPower;
    }

    // Getting the minimum shot power
    public float GetMinShootPower()
    {
        return _minShootPower;
    }

    // Unsubscribe from the shot power change event on disable
    private void OnDisable()
    {
        UIModel.OnFirePowerChanged -= ChangeFirePower;
    }
}