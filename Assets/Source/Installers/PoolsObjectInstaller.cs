using UnityEngine;
using Zenject;

public class PoolsObjectInstaller : MonoInstaller
{
    // Bullet Spawner and Effect Pool Manager for bullets to be embedded in the container
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private BulletEffectsPoolManager _bulletEffectsPoolManager;

    public override void InstallBindings()
    {
        // Register BulletSpawner and BulletEffectsPoolManager in the Zenject container as single instances      
        Container.Bind<BulletSpawner>().FromInstance(_bulletSpawner).AsSingle().NonLazy();
        Container.Bind<BulletEffectsPoolManager>().FromInstance(_bulletEffectsPoolManager).AsSingle().NonLazy();
    }
}