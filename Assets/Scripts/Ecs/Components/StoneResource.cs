using System;
using Core.Interfaces;
using UnityEngine;

namespace Ecs.Components
{
    [Serializable]
    public struct StoneResource : IEcsPoolTag
    {
        [field:SerializeField]
        public GameObject Object { get; set; }
    }
}