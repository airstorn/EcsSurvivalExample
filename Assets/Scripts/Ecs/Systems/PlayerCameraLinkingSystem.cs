using Ecs.Components;
using Ecs.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems
{
    public class PlayerCameraLinkingSystem : IEcsInitSystem
    {
        private readonly EcsFilter<CameraComponent, PlayerTag> _camera = null;
        private readonly EcsFilter<MovementComponent, PlayerTag> _player = null;
        
        public void Init()
        {
            foreach (var i in _player)
            {
                foreach (var c in _camera)
                {
                    ref var cameraComponent = ref _camera.Get1(c);
                    ref var playerComponent = ref _player.Get1(i);

                    cameraComponent.Camera.Follow = playerComponent.Controller.transform;
                    cameraComponent.Camera.LookAt = playerComponent.Controller.transform;
                }
            }
        }
    }
}