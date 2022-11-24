using Core;
using Ecs.Components;

namespace Ecs.Data
{
    public class PlayerOneFrames : OneFramesData
    {
        public override void ApplyOneFrames(EcsStartup ecsStartup)
        {
            ecsStartup.AddOneFrame<GatheringCanceledEvent>();
        }
    }
}