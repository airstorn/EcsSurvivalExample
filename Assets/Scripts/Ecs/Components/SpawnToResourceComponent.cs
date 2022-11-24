using System;
using Ecs.Mono;
using UnityEngine;

namespace Ecs.Components
{
    [Serializable]
    public struct SpawnToResourceComponent
    {
        public GameObject Self;
        
        public GameObject Resource;

        public int Count;

        public AnimationCurve Ease;

        public float SpawnRadius;

        public float JumpTime;

        public float SpawnHeight;
    }
}