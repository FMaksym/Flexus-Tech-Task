using UnityEngine;

public class BulletBouncer : MonoBehaviour
{
    private int bounceCount = 0;

    public float BounceDamping { get; set; } = 0.5f;
    public int MaxBounces { get; set; } = 2;
    public bool CanBounce => bounceCount < MaxBounces;

    // Method for collision processing and calculation of new velocity
    public Vector3 HandleCollision(Vector3 currentVelocity, Vector3 normal)
    {
        bounceCount++;

        // Return the velocity vector after bounce taking into account the damping
        return Vector3.Reflect(currentVelocity, normal) * BounceDamping;
    }

    // Method to check if the projectile should be deactivated (when the maximum number of bounces has been reached)
    public bool ShouldDeactivate()
    {
        return bounceCount >= MaxBounces;
    }

    // Reset the number of bounces, used when reactivating the projectile
    public void ResetBounces()
    {
        bounceCount = 0;
    }
}