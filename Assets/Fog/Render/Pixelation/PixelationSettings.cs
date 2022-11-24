using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Render.Pixelation
{
    [Serializable]
    public class PixelationSettings
    {
        public RenderPassEvent Event;
        public float Downsample;
    }
}