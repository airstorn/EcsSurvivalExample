using Core;
using Ecs.Data;
using Ecs.Systems;
using Utils;

namespace Zenject.Installers
{
    public class GameRulesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindToBaseClass<TimerSystem, EcsUpdateSystem>();
            
            Container.BindToBaseClass<GameOneFrames, OneFramesData>();
        }
    }
}