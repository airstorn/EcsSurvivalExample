using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Render.Pixelation
{
    public class PixelationPass : ScriptableRenderPass
    {
        private RenderTargetHandle _tempTarget;
        private Vector2 _rect;
        private float _downsample;
        
        public PixelationPass(PixelationSettings settings)
        {
            _downsample = settings.Downsample;
            _tempTarget.Init("_TempTexture");
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            _rect = new Vector2(cameraTextureDescriptor.width, cameraTextureDescriptor.height);
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            Vector2 downsampledTexture = _rect * _downsample;

            cmd.GetTemporaryRT(_tempTarget.id, (int)downsampledTexture.x, (int)downsampledTexture.y, 0, FilterMode.Point);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var colorTarget = renderingData.cameraData.renderer.cameraColorTarget;
            var depthTarget = renderingData.cameraData.renderer.cameraDepthTarget;

            CommandBuffer cmd = CommandBufferPool.Get("Pixelation");

            cmd.Blit(colorTarget, _tempTarget.Identifier());
            cmd.Blit(_tempTarget.Identifier(), colorTarget);
            
            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }
    }
}