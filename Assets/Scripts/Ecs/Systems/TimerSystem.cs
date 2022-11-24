using Core;
using Ecs.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems
{
    public class TimerSystem : EcsUpdateSystem
    {
        private readonly EcsFilter<TimerComponent> _filter = null;
        
        public override void Run()
        {
            foreach (var i in _filter)
            {
                ref var data = ref _filter.Get1(i);
                data.Current += Time.deltaTime;

                if (data.Current >= data.Target)
                {
                    ref var entity = ref _filter.GetEntity(i);
                    entity.Del<TimerComponent>();
                }
            }
        }
    }
}