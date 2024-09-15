using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public Vector3 Velocity { get; set; }
    public Vector3 Gravity { get; private set; } = new Vector3(0, -9.81f, 0);

    // Set the initial velocity of the projectile depending on the force and direction of the shot
    public void SetVelocity(float firePower, Vector3 direction)
    {
        Velocity = direction * firePower;
    }

    // Updating the velocity of the projectile taking into account gravity
    public void UpdateVelocity(float deltaTime)
    {
        Velocity += Gravity * deltaTime;
    }

    // Calculate the new position of the projectile based on the current velocity and time
    public Vector3 CalculateNewPosition(Vector3 currentPosition, float deltaTime)
    {
        return currentPosition + Velocity * deltaTime;
    }

    // Return the current velocity of the projectile
    public Vector3 GetVelocity()
    {
        return Velocity;
    }
}