using System;
using Cinemachine;

namespace Ecs.Components
{
    [Serializable]
    public struct CameraComponent
    {
        public CinemachineVirtualCamera Camera;
    }
}