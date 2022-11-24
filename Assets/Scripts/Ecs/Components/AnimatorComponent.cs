using System;
using UnityEngine;

namespace Ecs.Components
{
    [Serializable]
    public struct AnimatorComponent
    {
        public Animator Animator;

        public string SpeedKey;

        public string AttackKey;
    }
}