using Core;
using Ecs.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems
{
    public class TriggerDebugSystem : EcsUpdateSystem
    {
        private readonly EcsFilter<TriggerEnterEvent> _enterFilter = null;

        private readonly EcsFilter<TriggerExitEvent> _exitFilter = null;

        private bool _log;

        public TriggerDebugSystem(bool log)
        {
            _log = log;
        }

        public override void Run()
        {
            if (_log == false)
            {
                return;
            }
            
            foreach (var i in _enterFilter)
            {
                ref var data = ref _enterFilter.Get1(i);
                
                Debug.Log($"enter [{data.Other}] in trigger [{data.Trigger}]");
            } 
            
            foreach (var i in _exitFilter)
            {
                ref var data = ref _exitFilter.Get1(i);
                
                Debug.Log($"exit [{data.Other}] in trigger [{data.Trigger}]");
            }
        }
    }
}