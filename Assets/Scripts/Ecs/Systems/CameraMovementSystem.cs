using Core;
using Ecs.Components;
using Ecs.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems
{
    public class CameraMovementSystem : EcsUpdateSystem
    {
        private readonly EcsFilter<CameraComponent, PlayerTag> _filter = null;
        
        public override void Run()
        {
            foreach (var i in _filter)
            {
                ref var camera = ref _filter.Get1(i);
                camera.Camera.transform.position += Vector3.forward * Time.deltaTime;
            }
        }
    }
}