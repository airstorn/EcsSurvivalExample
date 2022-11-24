using System;
using UnityEngine;

namespace Ecs.Components
{
    [Serializable]
    public struct BodyGroundComponent
    {
        public bool IsGrounded;
        
        public Collider Trigger;

        public LayerMask InteractableLayers;

        public Vector3 Direction;

        public float Distance;
        
        public float Radius;
    }
}