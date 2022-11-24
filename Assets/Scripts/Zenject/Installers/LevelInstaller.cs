using Core;
using Ecs.Mono;
using Ecs.Providers;
using Ecs.Systems;
using Leopotam.Ecs;
using UnityEngine;

namespace Zenject.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField]
        private LevelInstanceProvider _prefab;

        [SerializeField]
        private DayNightCycleProvider _dayNightReference;
        
        public override void InstallBindings()
        {
            Container.Bind<LevelInstanceProvider>().FromComponentInNewPrefab(_prefab).AsSingle().NonLazy();

            DayNightCycleSystem dayNightCycleSystem = new DayNightCycleSystem(_dayNightReference);

            Container.BindInterfacesAndSelfTo<DayNightCycleSystem>().FromInstance(dayNightCycleSystem).AsSingle().NonLazy();
        }
    }
}