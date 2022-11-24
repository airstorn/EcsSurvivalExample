using UnityEngine;

namespace Ecs.Components
{
    public struct DayNightAnimationComponent
    {
        public float StartIntensity;
        
        public float TargetIntensity;

        public Color StartFogColor;
        
        public Color TargetFogColor;

        public Color StartLightColor;
        
        public Color TargetLightColor;
        
        public Light Light;

        public Material Fog;
    }
}