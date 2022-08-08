using Cinemachine;
using UnityEngine;

namespace Code.DataModule
{
    public class SceneData : MonoBehaviour
    {
        [Header("Player")]
        public Transform playerSpawnPoint;
        public FloatingJoystick playerJoystick;
        public CinemachineVirtualCamera Camera;
        
        [Header("Enemy Spawner")]
        public MeshCollider EnemyBasePlaneMesh;
    }
}