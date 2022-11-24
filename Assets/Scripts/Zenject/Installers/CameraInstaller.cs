using Core;
using Ecs.Providers;
using Ecs.Systems;
using UnityEngine;

namespace Zenject.Installers
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField]
        private CameraComponentProvider _playerCameraPrefab;

        public override void InstallBindings()
        {
            CameraSpawnSystem system = new CameraSpawnSystem(_playerCameraPrefab);

            Container
                .BindInterfacesAndSelfTo<CameraSpawnSystem>()
                .FromInstance(system)
                .AsSingle()
                .NonLazy();
        }
    }
}