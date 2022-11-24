using Ecs.Mono;
using Leopotam.Ecs;

namespace Ecs.Components
{
    public struct TriggerEnterEvent
    {
        public EcsEntity Other;
        public EcsEntity Trigger;
    }
}