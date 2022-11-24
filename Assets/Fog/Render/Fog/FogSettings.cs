using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Render.Fog
{
    [Serializable]
    public class FogSettings 
    {
        public RenderPassEvent Event;
        public Material Material;
    }
}