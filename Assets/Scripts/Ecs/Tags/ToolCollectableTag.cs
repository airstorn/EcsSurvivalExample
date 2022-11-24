using System;
using Ecs.Data;
using Leopotam.Ecs;

namespace Ecs.Tags
{
    [Serializable]
    public struct ToolCollectableTag : IEcsIgnoreInFilter
    {
        public ToolAsset VacantTool;

        public int HitsCount;
    }
}