using System;
using Ecs.Data;

namespace Ecs.Components
{
    [Serializable]
    public struct ToolContainerComponent
    {
        public ToolAsset Current;
    }
}