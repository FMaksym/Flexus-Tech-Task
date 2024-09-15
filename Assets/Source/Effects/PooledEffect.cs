using System;
using System.Threading.Tasks;
using UnityEngine;

public class PooledEffect : MonoBehaviour
{
    [SerializeField] private float _destroyTime; // Time after which the object is deactivated

    private Action _onDeactivateCallback; // Callback called when the object is deactivated to return to the pool

    // Method of object activation with a specified time of existence
    public async Task Activate(float destroyTime, Action onDeactivateCallback)
    {
        _destroyTime = destroyTime; // Set the time before deactivation
        _onDeactivateCallback = onDeactivateCallback; // Set the callback for deactivation
        gameObject.SetActive(true); // Activate the object
        
        // Start a timer to deactivate the object after a specified time
        await Task.Delay(TimeSpan.FromSeconds(_destroyTime));

        Deactivate(); // Call the deactivation method
    }

    // Deactivate the object and call the callback to return to the pool
    private void Deactivate()
    {
        gameObject.SetActive(false); // Deactivate the object
        _onDeactivateCallback?.Invoke(); // Call a callback to return the object to the pool
    }
}