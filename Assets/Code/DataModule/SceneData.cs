using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace Code.DataModule
{
    public class SceneData : MonoBehaviour
    {
        [Header("Player")]
        public Transform playerSpawnPoint;
        public FloatingJoystick playerJoystick;
        public CinemachineVirtualCamera Camera;
        public Button JumpButton;
        
        [Header("Enemy Spawner")]
        public MeshCollider EnemyBasePlaneMesh;
    }
}