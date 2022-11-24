using Core;
using Ecs.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems
{
    public class KeyboardInputSystem : EcsUpdateSystem
    {
        private readonly EcsFilter<InputComponent> _input = null;
        
        public override void Run()
        {
            foreach (var i in _input)
            {
                ref var input = ref _input.Get1(i);

                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                Vector2 direction = new Vector2(horizontal, vertical);

                input.Direction.x = direction.normalized.x * Mathf.Abs(horizontal);
                input.Direction.z = direction.normalized.y * Mathf.Abs(vertical);
            }
        }
    }
}