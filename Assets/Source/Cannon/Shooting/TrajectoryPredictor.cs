using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPredictor : MonoBehaviour
{
    [Header("Line")]
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _predictionPoints = 50; // Number of points for trajectory prediction
    [SerializeField] private float _timeStep = 0.1f; // Time step between points
    [SerializeField] private Vector3 _gravity = new Vector3(0, -9.81f, 0); // Gravity

    [Space(10)]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _firePower = 20f;
    [SerializeField] private LayerMask _collisionMask;

    private void OnEnable()
    {
        UIModel.OnFirePowerChanged += UpdateFirePower; // Subscribe to the event of change of fire power
    }

    private void Update()
    {
        // Update the trajectory on each frame
        Vector3 initialVelocity = _firePoint.forward * _firePower; // Calculate initial velocity
        PredictTrajectory(_firePoint.position, initialVelocity);
    }

    // Method for updating the shot force
    private void UpdateFirePower(float newFirePower)
    {
        _firePower = newFirePower;
    }

    // Method for predicting the trajectory of the projectile
    private void PredictTrajectory(Vector3 startPosition, Vector3 initialVelocity)
    {
        _lineRenderer.positionCount = _predictionPoints; // Set the number of points
        Vector3 currentPosition = startPosition; // Initial position
        Vector3 currentVelocity = initialVelocity; // Initial velocity

        for (int i = 0; i < _predictionPoints; i++)
        {
            // Calculate the new position taking into account velocity and gravity
            Vector3 newPosition = currentPosition + currentVelocity * _timeStep + 0.5f * _gravity * _timeStep * _timeStep;
            currentVelocity += _gravity * _timeStep;
            
            // Set the position of the point on the line
            _lineRenderer.SetPosition(i, currentPosition);

            // Check for collision
            if (Physics.Raycast(currentPosition, currentVelocity.normalized, out RaycastHit hit, (newPosition - currentPosition).magnitude, _collisionMask))
            {
                _lineRenderer.positionCount = i + 1; // Update the number of points in case of collision
                break; // Interrupt the loop if a collision occurs
            }

            currentPosition = newPosition; // Update the current position
        }
    }

    private void OnDisable()
    {
        UIModel.OnFirePowerChanged -= UpdateFirePower; // Unsubscribe from the fire power change event
    }
}