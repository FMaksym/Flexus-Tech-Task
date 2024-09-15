using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class ShootController : MonoBehaviour
{
    [SerializeField, Min(0), Tooltip("Time in miliseconds(example: 1s = 1000 ms)")] 
    private int _shootFrequency = 500;
    [SerializeField] private bool _CanShoot = true;

    [Header("Feedback Objects")]
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private CannonRecoil _cannonRecoil;

    [Inject] private BulletSpawner _bulletSpawner;

    // Method for executing a shot
    public async Task Shoot()
    {
        // Check if shooting is allowed
        if (_CanShoot)
        {
            // Disable the ability to fire until the current shot is completed
            _CanShoot = false;
            _bulletSpawner.GetBullet(); // Get a projectile from the pool and launch it
            _cameraShake.Shake(); // Perform camera shake
            _cannonRecoil.Recoil(); // Perform gun recoil

            // Wait for the end of the interval between shots
            await Task.Delay(_shootFrequency);
            _CanShoot = true; // Enable firing capability
        }
        else
        {
            return; // If firing is prohibited, exit the method
        }
    }
}