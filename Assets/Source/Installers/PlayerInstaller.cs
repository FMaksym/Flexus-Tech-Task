using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private ShootController _shootController; // ShootController to be embedded in the container

    public override void InstallBindings()
    {
        // Register the ShootController in the Zenject container as a single instance
        Container.Bind<ShootController>().
            FromInstance(_shootController). // Use an existing instance of _shootController
            AsSingle(). // Ensure that only one instance of the ShootController will be used
            NonLazy(); // Initialize the instance immediately when the container is started
    }
}