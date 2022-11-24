using Ecs.Providers;
using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Ecs.Systems
{
    public class CameraSpawnSystem : IEcsInitSystem
    {
        private readonly CameraComponentProvider _camera = null;

        private readonly EcsWorld _world = null;

        public CameraSpawnSystem(CameraComponentProvider provider)
        {
            _camera = provider;
        }

        public void Init()
        {
            var instance = GameObject.Instantiate(_camera);
            instance.Convert(_world);
        }
    }
}