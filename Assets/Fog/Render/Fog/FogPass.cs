using Render.Pixelation;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Render.Fog
{
    public class FogPass : ScriptableRenderPass
    {
        private RenderTargetHandle _tempTarget;
        private Material _material;
        
        public FogPass(FogSettings settings)
        {
            _material = settings.Material;
            _tempTarget.Init("_FogTexture");
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            cmd.GetTemporaryRT(_tempTarget.id, renderingData.cameraData.cameraTargetDescriptor);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var colorTarget = renderingData.cameraData.renderer.cameraColorTarget;
            var depthTarget = renderingData.cameraData.renderer.cameraDepthTarget;

            CommandBuffer cmd = CommandBufferPool.Get("Fog");
            
            cmd.SetGlobalTexture("_DepthTex", colorTarget);
            
            cmd.Blit(colorTarget, _tempTarget.Identifier(), _material);
            cmd.Blit(_tempTarget.Identifier(), colorTarget);

            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }
    }
}