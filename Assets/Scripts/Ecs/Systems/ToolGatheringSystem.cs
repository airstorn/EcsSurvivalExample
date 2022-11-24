using Core;
using Ecs.Components;
using Ecs.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems
{
    public class ToolGatheringSystem : EcsUpdateSystem
    {
        private readonly EcsFilter<TriggerEnterEvent, ToolContainerComponent>.Exclude<GatheringEvent> _filter = null;

        private readonly EcsFilter<TriggerExitEvent, ToolContainerComponent, GatheringEvent> _canceledFilter = null;

        private readonly EcsFilter<GatheringEvent> _eventFilter = null;

        public override void Run()
        {
            ProcessEvents();
            AnimateGatheringEvent();
        }

        private void ProcessEvents()
        {
            if (_filter.IsEmpty() && _canceledFilter.IsEmpty())
            {
                return;
            }
            
            foreach (var i in _filter)
            {
                ref var data = ref _filter.Get1(i);

                if (data.Trigger.Has<ToolCollectableTag>() == false)
                {
                    continue;
                }

                ref var tag = ref data.Trigger.Get<ToolCollectableTag>();
                ref var actorTool = ref _filter.Get2(i);
                ref var entity = ref _filter.GetEntity(i);

                if (tag.VacantTool == actorTool.Current)
                {
                    ref var eventData = ref entity.Get<GatheringEvent>();
                    eventData.Target = data.Trigger;
                    eventData.ElapsedTime = 0;
                    eventData.HitCount = tag.HitsCount;
                    eventData.Tool = tag.VacantTool;
                }
            }

            foreach (var i in _canceledFilter)
            {
                ref var gatheringEvent = ref _canceledFilter.Get3(i);
                ref var exitEvent = ref _canceledFilter.Get1(i);

                if (exitEvent.Trigger != gatheringEvent.Target)
                {
                    continue;
                }
                
                ref var entity = ref _canceledFilter.GetEntity(i);
                entity.Del<GatheringEvent>();
                entity.Get<GatheringCanceledEvent>();
            }
        }

        private void AnimateGatheringEvent()
        {
            foreach (var i in _eventFilter)
            {
                ref var data = ref _eventFilter.Get1(i);

                data.ElapsedTime += Time.deltaTime;

                if (data.ElapsedTime >= (data.HitCount * (data.Tool.HitDelay + data.Tool.ReloadTime)) - data.Tool.ReloadTime)
                {
                    data.Target.Get<DestroyedEvent>();
                    ref var eventOwner = ref _eventFilter.GetEntity(i);

                    eventOwner.Get<GatheringCanceledEvent>();

                    _eventFilter.GetEntity(i).Del<GatheringEvent>();
                }
            }
        }
    }
}