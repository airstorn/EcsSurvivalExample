using System;
using UnityEngine;

namespace Ecs.Components
{
    [Serializable]
    public struct ToolPresenterComponent
    {
        public Transform Parent;

        public GameObject CurrentInstance;
    }
}