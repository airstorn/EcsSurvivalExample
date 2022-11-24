using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Render.Pixelation
{
    public class Pixelate : ScriptableRendererFeature
    {
        [SerializeField] private PixelationSettings _settings;

        private PixelationPass _pass;
        
        public override void Create()
        {
            _pass = new PixelationPass(_settings)
            {
                renderPassEvent = _settings.Event
            };
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(_pass);
        }
    }
}


