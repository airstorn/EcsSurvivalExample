using Ecs.Providers;
using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Ecs.Systems
{
    public class PlayerSpawnSystem : IEcsInitSystem
    {
        private readonly MovementProvider _movementProvider;

        private readonly EcsWorld _world = null;
        
        public PlayerSpawnSystem(MovementProvider movementProvider)
        {
            _movementProvider = movementProvider;
        }
        
        public void Init()
        {
            var instance = GameObject.Instantiate(_movementProvider);
            instance.Convert(_world);
        }
    }
}