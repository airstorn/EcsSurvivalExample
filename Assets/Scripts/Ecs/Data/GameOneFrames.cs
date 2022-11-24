using Core;
using Ecs.Components;

namespace Ecs.Data
{
    public class GameOneFrames : OneFramesData
    {
        public override void ApplyOneFrames(EcsStartup ecsStartup)
        {
            ecsStartup.AddOneFrame<EquipToolEvent>();
            ecsStartup.AddOneFrame<SpawnedEvent>();
        }
    }
}