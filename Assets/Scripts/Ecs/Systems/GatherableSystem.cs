using Core;
using Core.Interfaces;
using DG.Tweening;
using Ecs.Components;
using Ecs.Mono;
using Ecs.Tags;
using Leopotam.Ecs;
using UnityEngine;
using Zenject.Pool;

namespace Ecs.Systems
{
    public class GatherableSystem : EcsUpdateSystem
    {
        private readonly EcsFilter<DestroyedEvent, ToolCollectableTag, SpawnToResourceComponent> _destroyed = null;

        private EcsPoolsUtility _poolsUtility;
        
        public GatherableSystem(EcsPoolsUtility poolsUtility)
        {
            _poolsUtility = poolsUtility;
        }
        
        public override void Run()
        {
            foreach (var i in _destroyed)
            {
                ref var data = ref _destroyed.Get3(i);
                ref var entity = ref _destroyed.GetEntity(i);
                Vector3 spawnPos = data.Self.transform.position;

                entity.Del<DestroyedEvent>();
                var type = data.Resource.GetComponent<IEcsPoolTagProvider>();

                data.Self.transform.position += Vector3.down * 100;

                for (int index = 0; index < data.Count; index++)
                {
                    if (_poolsUtility.TryGetPool(type.Tag, out IEcsPool pool))
                    {
                        var instance = pool.GetEntity();

                        var tag = instance.GetTagFromEntity();
                        
                        tag.Object.transform.position = spawnPos;

                        float itemNormalized = index / (float)data.Count;

                        float angle = Mathf.Lerp(0, 360, itemNormalized);

                        tag.Object.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);

                        Vector3 targetPos =
                            new Vector3(
                                spawnPos.x + (Mathf.Sin(angle * Mathf.Deg2Rad) * data.SpawnRadius),
                                spawnPos.y,
                                spawnPos.z + (Mathf.Cos(angle * Mathf.Deg2Rad)) * data.SpawnRadius);

                        tag.Object.transform.DOJump(targetPos, data.SpawnHeight, 1, data.JumpTime).SetEase(data.Ease);
                    }
                }
            }
        }
    }

    public static class PullExtension
    {
        public static IEcsPoolTag GetTagFromEntity(this ref EcsEntity entity)
        {
            object[] components = null;
            int count = entity.GetComponentValues(ref components);

            for (int i = 0; i < count; i++)
            {
                if (components[i] is IEcsPoolTag tag)
                {
                    return tag;
                }
            }

            return null;
        }
    }
}