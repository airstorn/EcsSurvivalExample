using System;
using Core.Interfaces;
using Ecs.Components;
using Ecs.Mono;
using Ecs.Providers;
using UnityEngine;
using Zenject.Pool;

namespace Zenject.Installers
{
    public class PoolsInstaller : MonoInstaller
    {
        [Serializable]
        private class PoolItem<T> where T : struct, IEcsPoolTag
        {
            public PullableMonoConverter<T>[] Variants;

            public int Size;
        }
        
        
        [SerializeField]
        private PoolItem<WoodResource> _wood;

        [SerializeField]
        private PoolItem<StoneResource> _stone;

        public override void InstallBindings()
        {
            Container.Bind<EcsPoolsUtility>().FromNew().AsSingle().NonLazy();
            
            CreatePool(_wood);
            CreatePool(_stone);
        }

        private void CreatePool<T>(PoolItem<T> data) where T : struct, IEcsPoolTag
        {
            var folder = new GameObject(typeof(T).Name).transform;
            folder.SetParent(transform);
            
            EcsPoolSystem<T> pool = new EcsPoolSystem<T>(data.Variants, data.Size, folder);

            Container.BindInterfacesAndSelfTo<EcsPoolSystem<T>>().FromInstance(pool).AsSingle().NonLazy();
        }
    }
}