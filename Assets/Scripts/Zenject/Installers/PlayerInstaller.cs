using Core;
using Ecs.Data;
using Ecs.Providers;
using Ecs.Systems;
using UnityEngine;
using Utils;

namespace Zenject.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private MovementProvider _reference;

        [SerializeField]
        private GameObject _ui;
        
        public override void InstallBindings()
        {
            Container.BindToBaseClass<PlayerOneFrames, OneFramesData>();

            BindPlayer();
            BindCombat();
            BindView();
            BindUI();
        }

        private void BindPlayer()
        {
            PlayerSpawnSystem system = new PlayerSpawnSystem(_reference);
            PlayerCameraLinkingSystem cameraLinking = new PlayerCameraLinkingSystem();

            Container
                .BindInterfacesAndSelfTo<PlayerSpawnSystem>()
                .FromInstance(system)
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<PlayerCameraLinkingSystem>()
                .FromInstance(cameraLinking)
                .AsSingle()
                .NonLazy();
            
        }

        private void BindView()
        {
            Container.BindToBaseClass<PlayerAnimationSystem, EcsUpdateSystem>();
        }

        private void BindCombat()
        {
            Container.BindInterfacesAndSelfTo<ToolEquipmentSystem>().AsSingle().NonLazy();
            
            Container.BindToBaseClass<ToolGatheringSystem, EcsUpdateSystem>();
            
            Container.BindToBaseClass<GatherableSystem, EcsUpdateSystem>();
            
            Container.BindToBaseClass<CollectableSystem, EcsUpdateSystem>();
        }
        
        private void BindUI()
        {
            PlayerUiSystem system = new PlayerUiSystem(_ui);
            Container.BindInterfacesTo<PlayerUiSystem>().FromInstance(system).AsSingle().NonLazy();
        }
    }
}