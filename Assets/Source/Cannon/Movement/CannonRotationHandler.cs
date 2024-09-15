using UnityEngine;

public class CannonRotationHandler : IRotationHandler
{
    private Transform cannonTransform;
    private Vector2 targetDirection;
    private Vector2 targetCannonDirection;

    // Class constructor for initializing the direction and transform of the cannon
    public CannonRotationHandler(Transform transform, GameObject cannonBase)
    {
        // Maintain the initial direction of the gun (local rotation)
        targetDirection = transform.localRotation.eulerAngles;
        cannonTransform = transform;

        // If a character object is passed, keep its initial direction
        if (cannonBase) 
            targetCannonDirection = cannonBase.transform.localRotation.eulerAngles;
    }

    // Method for applying cannon and cannon base rotation based on input data (e.g. gestures)
    // touchFinal - final values for X and Y rotation
    // cannonBase - cannon base object (if any)
    // clampInDegrees - rotation restrictions by angles for cannon and cannon base
    public void ApplyRotation(Vector2 touchFinal, GameObject cannonBase, Vector2 clampInDegrees)
    {
        // Limit rotation in the X and Y axes based on the passed limits
        if (clampInDegrees.x < 360)
            touchFinal.x = Mathf.Clamp(touchFinal.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

        if (clampInDegrees.y < 360)
            touchFinal.y = Mathf.Clamp(touchFinal.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

        // Determine the orientation and rotate the cannon
        var targetOrientation = Quaternion.Euler(targetDirection);
        cannonTransform.localRotation = Quaternion.AngleAxis(-touchFinal.y, targetOrientation * Vector3.right) * targetOrientation;

        // Determine the orientation for cannon rotation (take the base of the cannon as a basis)
        var targetCharacterOrientation = Quaternion.Euler(targetCannonDirection);
        Quaternion yRotation = Quaternion.identity;


        // If there is a cannon base, rotate it along the Y axis. If not, rotate the cannon along the Y axis
        if (cannonBase)
        {
            yRotation = Quaternion.AngleAxis(touchFinal.x, Vector3.up);
            cannonBase.transform.localRotation = yRotation * targetCharacterOrientation;
        }
        else
        {
            yRotation = Quaternion.AngleAxis(touchFinal.x, cannonTransform.InverseTransformDirection(Vector3.up));
            cannonTransform.localRotation *= yRotation;
        }
    }
}