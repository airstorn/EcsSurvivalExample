using Leopotam.Ecs;

namespace Ecs.Components
{
    public struct TriggerExitEvent
    {
        public EcsEntity Other;

        public EcsEntity Trigger;
    }
}