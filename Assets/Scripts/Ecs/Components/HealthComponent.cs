using System;

namespace Ecs.Components
{
    [Serializable]
    public struct HealthComponent
    {
        public float Max;

        public float Current;
    }
}