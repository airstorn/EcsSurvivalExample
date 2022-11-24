using System;
using Core;
using Core.Interfaces;
using Ecs.Components;
using Ecs.Mono;
using Ecs.Tags;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Zenject.Pool
{
    public interface IEcsPool
    {
        EcsEntity GetEntity();
        bool IsEqual<T>(T obj);
    }
    public class EcsPoolSystem<T> : EcsUpdateSystem,
        IEcsInitSystem,
        IEcsPool
        where T : struct, IEcsPoolTag
    {
        private readonly EcsFilter<PulledTag, T> _pull = null;

        private readonly EcsFilter<T, DestroyedEvent>.Exclude<PulledTag> _pushingFilter = null;

        private readonly EcsFilter<T, SpawnedEvent> _spawnedFilter = null;

        private readonly EcsWorld _world;

        private readonly int _initialSize;

        private Transform _root;

        [EcsIgnoreInject]
        private readonly MonoConverter<T>[] _variants;


        public EcsPoolSystem(MonoConverter<T>[] variants, int initialSize, Transform root)
        {
            _variants = variants;
            _initialSize = initialSize;
            _root = root;
        }

        public void Init()
        {
            ExpandBy(_initialSize);
        }
        
        public override void Run()
        {
            foreach (var i in _pushingFilter)
            {
                ref var entity = ref _pushingFilter.GetEntity(i);
                ref var data = ref _pushingFilter.Get1(i);

                entity.Get<PulledTag>();
                entity.Del<DestroyedEvent>();

                data.Object.SetActive(false);
                data.Object.transform.SetParent(_root);
            }

            foreach (var i in _spawnedFilter)
            {
                ref var data = ref _spawnedFilter.Get1(i);
                ref var entity = ref _spawnedFilter.GetEntity(i);
                
                entity.Del<PulledTag>();
                data.Object.transform.SetParent(null);
                data.Object.SetActive(true);
            }
        }

        private MonoConverter<T> GetRandomItem()
        {
            return _variants[Random.Range(0, _variants.Length)];
        }
        
        private void ExpandBy(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var instance = GameObject.Instantiate(GetRandomItem());

                if (instance.TryGetComponent(out EcsEntityProvider provider))
                {
                    instance.Convert(_world);
                }
                else
                {
                    provider = instance.AddComponent<EcsEntityProvider>();
                    instance.Convert(_world);
                }

                ref var obj = ref provider.Entity.Get<T>();
                
                obj.Object.SetActive(false);
                obj.Object.transform.SetParent(_root);
            }
        }
        
        public bool IsEqual<T1>(T1 obj)
        {
            return obj.GetType() == typeof(T);
        }


        public EcsEntity GetEntity()
        {
            if (_pull.IsEmpty())
            {
                ExpandBy(1);
            
                foreach (var i in _pushingFilter)
                {
                    ref var entity = ref _pushingFilter.GetEntity(i);
                    entity.Del<DestroyedEvent>();
                    entity.Get<PulledTag>();
                }
            }
            
            foreach (var i in _pull)
            {
                ref var entity = ref _pull.GetEntity(i);

                entity.Get<SpawnedEvent>();
                Run();
                return entity;
            }

            throw new Exception();
        }
    }
}