using Core;
using Ecs.Components;
using Ecs.Providers;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems
{
    public class ColliderGroundnessSystem : EcsUpdateSystem
    {
        private readonly EcsFilter<BodyGroundComponent, InputComponent> _player = null;
        private readonly EcsFilter<BodyGroundComponent> _filter = null;

        public override void Run()
        { 
            ProcessGroundess();
            ProcessPlayerGravity();
        }

        private void ProcessGroundess()
        {
            foreach (var i in _filter)
            {
                ref var data = ref _filter.Get1(i);

                data.IsGrounded = Physics.SphereCast(data.Trigger.bounds.center, data.Radius, data.Direction, out RaycastHit hit, data.Distance, data.InteractableLayers);
            }
        }

        private void ProcessPlayerGravity()
        {
            foreach (var i in _player)
            {
                ref var physics = ref _player.Get1(i);
                ref var input = ref _player.Get2(i);

                if (physics.IsGrounded)
                {
                    input.Direction.y = 0;
                    return;
                }

                input.Direction.y += Physics.gravity.y;
            }
        }
    }
}