using UnityEngine;

namespace Code.ViewModule
{
    public class WeaponView : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public Transform bulletParent;
        public float bulletSpeed;
        public float weaponDamage;
        public int bulletsDeactivationTimeSec;
        public float bulletsIntervalTimeSec;
        public Vector3 shootDirection;
    }
}