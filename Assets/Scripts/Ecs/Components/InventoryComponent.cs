using System.Collections.Generic;
using Ecs.Data;

namespace Ecs.Components
{
    public struct InventoryComponent
    {
        public Dictionary<ResourceType, int> Storage;
    }
}