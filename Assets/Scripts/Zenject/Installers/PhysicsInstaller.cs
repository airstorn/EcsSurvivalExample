using Core;
using Ecs.Data;
using Ecs.Systems;
using UnityEngine;
using Utils;

namespace Zenject.Installers
{
    public class PhysicsInstaller : MonoInstaller
    {
        [SerializeField]
        private bool _logTriggers;
        
        public override void InstallBindings()
        {
            Container.BindToBaseClass<PhysicsOneFrames, OneFramesData>();
            Container.BindToBaseClass<ColliderGroundnessSystem, EcsUpdateSystem>();

            TriggerDebugSystem debugSystem = new TriggerDebugSystem(_logTriggers);
            
            Container.BindToBaseClass<TriggerDebugSystem, EcsUpdateSystem>(debugSystem);
        }
    }
}