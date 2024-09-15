using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly Queue<T> _pool = new Queue<T>(); // Queue for storing pool objects
    private readonly T _prefab; // Prefab from which objects are created
    private readonly Transform _parent; // Parent object for created objects

    // Constructor of the class that initializes the object pool
    public ObjectPool(T prefab, int initialCount, Transform parent = null)
    {
        _prefab = prefab; // Set the prefab for creating objects
        _parent = parent; // Set the parent object

        // Initialize the pool, creating the specified number of objects
        for (int i = 0; i < initialCount; i++)
        {
            var obj = CreateNewObject(); // Create a new object
            _pool.Enqueue(obj); // Add an object to the pool
        }
    }

    // Method for creating a new object from a prefab
    private T CreateNewObject()
    {
        T obj = GameObject.Instantiate(_prefab, _parent); // Create a new object and set the parent
        obj.gameObject.SetActive(false); // Deactivate the object
        return obj; // Return the created object
    }

    // Method for getting an object from the pool   
    public T GetFromPool()
    {
        if (_pool.Count > 0)
        {
            return _pool.Dequeue(); // If there are objects in the pool, return one of them
        }
        else
        {
            return CreateNewObject(); // If the pool is empty, create a new object
        }
    }

    // Method for returning an object to the pool
    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false); // Deactivate the object
        _pool.Enqueue(obj); // Add the object back to the pool
    }
}