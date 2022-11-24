using Ecs.Data;
using Leopotam.Ecs;

namespace Ecs.Components
{
    public struct GatheringEvent
    {
        public EcsEntity Target;

        public ToolAsset Tool;

        public float ElapsedTime;

        public int HitCount;
    }
}