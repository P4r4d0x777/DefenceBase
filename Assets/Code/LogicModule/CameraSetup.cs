using Cinemachine;
using UnityEngine;

namespace Code.LogicModule
{
    public static class CameraSetup
    {
        public static void Setup(CinemachineVirtualCamera Camera, Transform transform)
        {
            Camera.Follow = transform;
            Camera.LookAt = transform;
        }
    }
}