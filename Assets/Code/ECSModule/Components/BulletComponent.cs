using UnityEngine;

namespace Code.ECSModule.Components
{
    public struct BulletComponent
    {
        public Transform bulletTransform;
        public GameObject bulletGameObject;
        public int bulletsDeactivationTimeSec;
        public float speed;
        public float damage;
    }
}