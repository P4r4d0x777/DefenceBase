using UnityEngine;

namespace Code.ECSModule.Components
{
    public struct WeaponComponent
    {
        public GameObject bulletPrefab;
        public Transform bulletParent;
        public float bulletSpeed;
        public float weaponDamage;
        public int bulletsDeactivationTimeSec;
        public float bulletsIntervalTimeSec;
    }
}