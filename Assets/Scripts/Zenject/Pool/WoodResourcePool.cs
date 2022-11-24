using System;
using Core.Interfaces;
using UnityEngine;

namespace Zenject.Pool
{
    [Serializable]
    public struct WoodResource : IEcsPoolTag
    {
        [field:SerializeField]
        public GameObject Object { get; set; }
    }
}
