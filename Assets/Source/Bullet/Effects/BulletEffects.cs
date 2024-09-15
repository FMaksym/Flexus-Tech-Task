using UnityEngine;

public class BulletEffects : MonoBehaviour
{
    private BulletEffectsPoolManager _effectsPoolManager;

    public void Initialize(BulletEffectsPoolManager bulletEffectsPoolManager) // Initialization of the effect pool field
    {
        _effectsPoolManager = bulletEffectsPoolManager;
    }

    // Method for creating a decal(hit effect)
    public void CreateDecal(Vector3 position, Vector3 normal) // Method for creating a decal in a given position and orientation
    {
        Decal decal = _effectsPoolManager.GetDecalPool().GetFromPool(); // Get the decal object from the pool and cast it to Decal type

        if (decal != null)
        {
            // Activate the decal with a timer for 3 seconds and return it to the pool when the time expires
            decal.Activate(3f, () => _effectsPoolManager.GetDecalPool().ReturnToPool(decal));

            // Configure position and orientation of the decal
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, normal);
            decal.transform.rotation = rotation;
            decal.transform.position = position + normal * 0.01f;
        }
    }

    // Method for creating an explosion effect
    public void CreateExplosion(Vector3 position, Vector3 normal)
    {
        // Get explosion object from the pool and set it to Explosion type
        Explosion explosion = _effectsPoolManager.GetExplosionPool().GetFromPool() as Explosion;

        if (explosion != null)
        {
            // Activate the explosion with a timer for 2 seconds and return it to the pool when the time expires
            explosion.Activate(2f, () => _effectsPoolManager.GetExplosionPool().ReturnToPool(explosion));

            // Configure position and orientation of the explosion object
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, normal);
            explosion.transform.rotation = rotation;
            explosion.transform.position = position;
        }
    }
}