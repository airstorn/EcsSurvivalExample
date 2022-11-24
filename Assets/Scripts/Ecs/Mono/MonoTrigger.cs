using Ecs.Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Ecs.Mono
{
    public class MonoTrigger : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _mask;

        [SerializeField]
        private EcsEntityProvider _selfEntity;

        private EcsWorld _world;

        [Inject]
        private void Construct(EcsStartup ecs)
        {
            _world = ecs.World;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if((_mask.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                if (other.TryGetComponent(out EcsEntityProvider provider))
                {
                    var entity = provider.Entity;
                    ref var ecsEvent = ref entity.Get<TriggerEnterEvent>();

                    ecsEvent.Other = provider.Entity;
                    ecsEvent.Trigger = _selfEntity.Entity;
                }
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if((_mask.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                if (other.TryGetComponent(out EcsEntityProvider provider))
                {
                    var entity = provider.Entity;
                    ref var ecsEvent = ref entity.Get<TriggerExitEvent>();

                    ecsEvent.Other = provider.Entity;
                    ecsEvent.Trigger = _selfEntity.Entity;
                }
            }
        }
    }
}