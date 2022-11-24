using Core;
using DG.Tweening;
using Ecs.Components;
using Ecs.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems
{
    public class CollectableSystem : EcsUpdateSystem
    {
        private readonly EcsFilter<MovementComponent, PlayerTag> _flyTarget = null;
        
        private readonly EcsFilter<CollectableAnimationComponent>.Exclude<TimerComponent, PulledTag> _completed = null;
        
        private readonly EcsFilter<CollectableAnimationComponent, TimerComponent> _animated = null;

        private readonly EcsFilter<CollectableAnimationComponent>.Exclude<TimerComponent> _delayed = null;
        
        private readonly EcsFilter<CollectableAnimationComponent, SpawnedEvent> _filter = null;

        public override void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var component = ref _filter.Get1(i);

                ref var timer = ref entity.Get<TimerComponent>();

                timer.Target = Random.Range(component.MinDelayBeforeFly, component.MaxDelayBeforeFly);

                component.IsWaiting = true;
            }

            foreach (var i in _delayed)
            {
                ref var component = ref _delayed.Get1(i);

                if (component.IsWaiting)
                {
                    ref var entity = ref _delayed.GetEntity(i);

                    ref var timer = ref entity.Get<TimerComponent>();

                    component.Origin = component.Movable.position;
                    timer.Target = component.FlyTime;
                    component.IsWaiting = false;
                }
            }

            foreach (var i in _animated)
            {
                ref var component = ref _animated.Get1(i);

                if (component.IsWaiting)
                {
                    continue;
                }

                ref var timer = ref _animated.Get2(i);

                float normalized = timer.Current / timer.Target;

                foreach (var t in _flyTarget)
                {
                    ref var target = ref _flyTarget.Get1(i);

                    var pos = Vector3.Lerp(component.Origin, target.Controller.transform.position + target.Controller.center, normalized);

                    component.Movable.position = pos;
                }
            }

            foreach (var i in _completed)
            {
                ref var component = ref _completed.Get1(i);

                if (component.IsWaiting == false)
                {
                    ref var entity = ref _completed.GetEntity(i);
                    entity.Get<DestroyedEvent>();
                }
            }
        }
    }
}