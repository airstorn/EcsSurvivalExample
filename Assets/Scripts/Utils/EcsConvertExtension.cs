using Ecs.Mono;
using Leopotam.Ecs;
using UnityEngine;

namespace Utils
{
    public static class EcsConvertExtension
    {
        public static void Convert(this MonoConverter converter, EcsWorld world)
        {
            EcsEntity entity = world.NewEntity();
            var entityComponents = converter.GetComponents<MonoConverter>();

            if (converter.TryGetComponent(out EcsEntityProvider provider))
            {
                provider.Entity = entity;
            }
            
            foreach (var component in entityComponents)
            {
                component.Convert(entity);
                GameObject.Destroy(component);
            }
        }
    }
}