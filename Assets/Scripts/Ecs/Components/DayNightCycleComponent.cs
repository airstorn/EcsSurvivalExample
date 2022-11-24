using System;
using Ecs.Mono;
using UnityEngine;

namespace Ecs.Components
{
    [Serializable]
    public struct DayNightCycleComponent
    {
        public float DayTime;

        public float NightTime;

        public float DayLightIntensity;
        
        public float NightLightIntensity;

        public float TransitionTime;

        public Light DayLight;

        public Material FogMaterial;

        public Color DayFog;
        
        public Color NightFog;

        public Color DayLightColor;
        public Color NightLightColor;

        public DayTime CurrentTime;
    }
}