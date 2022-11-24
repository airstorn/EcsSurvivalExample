using System;
using UnityEngine;

namespace Ecs.Components
{
    [Serializable]
    public struct CollectableAnimationComponent
    {
        public float MinDelayBeforeFly;
        
        public float MaxDelayBeforeFly;

        public float FlyTime;

        public Vector3 Origin;
        
        public Transform Movable;

        public bool IsWaiting;
    }
}