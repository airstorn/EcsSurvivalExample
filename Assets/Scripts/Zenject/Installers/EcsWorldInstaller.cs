using Leopotam.Ecs;
using UnityEngine;

namespace Zenject.Installers
{
    public class EcsWorldInstaller : MonoInstaller
    {
        [SerializeField]
        private EcsStartup _reference;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(new EcsWorld());
            
            Container
                .BindInterfacesAndSelfTo<EcsStartup>()
                .AsSingle()
                .NonLazy();
        }
    }
}