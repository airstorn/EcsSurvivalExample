using System;
using UnityEngine;

namespace Ecs.Components
{
    [Serializable]
    public struct MovementComponent
    {
        public float Speed;
        public CharacterController Controller;
    }
}