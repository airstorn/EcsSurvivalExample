using Core;
using Ecs.Components;
using Ecs.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems
{
    public class PlayerInputSystem : EcsUpdateSystem
    {
        private readonly EcsFilter<InputComponent> _inputFilter = null;
        private readonly EcsFilter<MovementComponent, PlayerTag> _player = null;
        
        public override void Run()
        {
            foreach (var i in _inputFilter)
            {
                foreach (var p in _player)
                {
                    ref var input = ref _inputFilter.Get1(i);
                    ref var player = ref _player.Get1(p);

                    Vector3 direction = new Vector3();

                    direction.x = input.Direction.x * player.Speed;
                    direction.z = input.Direction.z * player.Speed;

                    if (direction != Vector3.zero)
                    {
                        player.Controller.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
                    }

                    direction.y = input.Direction.y;
                    direction *= Time.deltaTime;
                    
                    player.Controller.Move(direction);
                }
            }
        }
    }
}