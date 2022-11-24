using Core;
using Ecs.Components;

namespace Ecs.Data
{
    public class PhysicsOneFrames : OneFramesData
    {
        public override void ApplyOneFrames(EcsStartup ecsStartup)
        {
            ecsStartup.AddOneFrame<TriggerEnterEvent>();
            ecsStartup.AddOneFrame<TriggerExitEvent>();
        }
    }
}