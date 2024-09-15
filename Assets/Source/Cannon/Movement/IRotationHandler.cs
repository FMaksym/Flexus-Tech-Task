using UnityEngine;

public interface IRotationHandler
{
    // Method for applying rotation to an object based on touch inputs
    void ApplyRotation(Vector2 touchFinal, GameObject cannonBase, Vector2 clampInDegrees);
}