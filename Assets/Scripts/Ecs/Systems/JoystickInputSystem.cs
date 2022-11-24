using Core;
using Ecs.Components;
using Ecs.Providers;
using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Ecs.Mono
{
    public class JoystickInputSystem : EcsUpdateSystem,
        IEcsInitSystem
    {
        private readonly EcsFilter<InputComponent> _input = null;
        private readonly EcsFilter<JoystickInputComponent> _filter = null;

        private readonly JoystickInputProvider _provider;

        private readonly EcsWorld _world = null;
        
        public JoystickInputSystem(JoystickInputProvider provider)
        {
            _provider = provider;
        }
        
        public void Init()
        {
            var instance = GameObject.Instantiate(_provider);
            instance.Convert(_world);
        }

        public override void Run()
        {
            foreach (var f in _filter)
            {
                foreach (var i in _input)
                {
                    ref var joystick = ref _filter.Get1(f);
                    ref var input = ref _input.Get1(i);

                    input.Direction.x = joystick.Joystick.Direction.x;
                    input.Direction.z = joystick.Joystick.Direction.y;
                }
            }
        }
    }
}