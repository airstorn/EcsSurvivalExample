using Render.Pixelation;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Render.Fog
{
    public class FogRender : ScriptableRendererFeature
    {
        [SerializeField] private FogSettings _settings;

        private FogPass _pass;

        public override void Create()
        {
            _pass = new FogPass(_settings)
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