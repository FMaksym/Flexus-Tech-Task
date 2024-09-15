using UnityEngine;

[RequireComponent(typeof(BulletMover), typeof(BulletBouncer), typeof(BulletEffects))]
public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float _firePower = 10f;
    [SerializeField] private float _maxDetectionRayDistance = 0.5f;
    [SerializeField] private LayerMask _collisionMask;

    [Header("Bullet Move Components")]
    [SerializeField] private BulletMover _bulletMover;
    [SerializeField] private BulletBouncer _bulletBouncer;
    [SerializeField] private BulletEffects _bulletEffects;

    private BulletSpawner _bulletSpawner;
    private BulletEffectsPoolManager _effectsPoolManager;

    private void OnEnable()
    {
        UIModel.OnFirePowerChanged += UpdateFirePower; // Subscribe to the projectile power change event
        InitializeComponents();
    }

    // Initialization of the projectile during creation
    public void Initialize(BulletSpawner bulletSpawner, float firePower, BulletEffectsPoolManager effectsPoolManager)
    {
        _bulletSpawner = bulletSpawner;
        _bulletEffects.Initialize(effectsPoolManager);
        UpdateFirePower(firePower);
    }

    // Initialization of necessary components
    private void InitializeComponents()
    {
        _bulletMover.SetVelocity(_firePower, transform.forward);
    }

    // Update projectile firing power and initialize components
    public void UpdateFirePower(float firePower)
    {
        _firePower = firePower;
        InitializeComponents();
    }

    private void FixedUpdate()
    {
        SimulateBullet(Time.fixedDeltaTime); // Simulation of projectile motion every physical frame
    }

    // Simulation of projectile motion
    private void SimulateBullet(float deltaTime)
    {
        _bulletMover.UpdateVelocity(deltaTime); // Update projectile velocity
        Vector3 newPosition = _bulletMover.CalculateNewPosition(transform.position, deltaTime); // Calculate new position

        // Surface collision check
        Ray ray = new Ray(transform.position, _bulletMover.Velocity.normalized);
        if (Physics.Raycast(ray, out RaycastHit hit, _maxDetectionRayDistance, _collisionMask))
        {
            _bulletEffects.CreateDecal(hit.point, hit.normal); // Creating a collision trace

            // Handling projectile rebound
            _bulletMover.Velocity = _bulletBouncer.HandleCollision(_bulletMover.Velocity, hit.normal);
            newPosition = hit.point;

            // If the projectile is to be deactivated (has reached the maximum number of bounces)
            if (_bulletBouncer.ShouldDeactivate())
            {
                // Creating an explosion and trace. Return the projectile to the pool
                _bulletEffects.CreateExplosion(hit.point, hit.normal);
                _bulletEffects.CreateDecal(hit.point, hit.normal);

                _bulletBouncer.ResetBounces();
                _bulletSpawner.ReturnToPool(gameObject);
                return;
            }
        }

        transform.position = newPosition; // Update projectile position
    }

    private void OnDisable()
    {
        UIModel.OnFirePowerChanged -= UpdateFirePower; // Unsubscribe from the projectile power change event when the object is deactivated
    }
}