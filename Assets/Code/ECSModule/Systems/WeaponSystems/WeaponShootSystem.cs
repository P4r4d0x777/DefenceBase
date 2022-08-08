using Code.ECSModule.Components;
using Code.ECSModule.Events;
using Code.ECSModule.Requests;
using Code.ProvidersModule;
using Leopotam.Ecs;
using NTC.Global.Pool;

namespace Code.ECSModule.Systems.WeaponSystems
{
    public class WeaponShootSystem : IEcsRunSystem
    {
        private EcsFilter<WeaponComponent, WeaponShootEvent>.Exclude<WeaponDelayShootingRequest> _filter;
        private EcsWorld _ecsWorld;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var weapon = ref _filter.Get1(i);
                ref var weaponEntity = ref _filter.GetEntity(i);
                
                var bulletGameObject = NightPool.Spawn(weapon.bulletPrefab, weapon.bulletParent.position, weapon.bulletParent.rotation);
                var bulletEntity = _ecsWorld.NewEntity();

                ref var bullet = ref bulletEntity.Get<BulletComponent>();

                //bullet.direction = weapon.bulletParent.transform;
                bullet.damage = weapon.weaponDamage;
                bullet.speed = weapon.bulletSpeed;
                bullet.bulletsDeactivationTimeSec = weapon.bulletsDeactivationTimeSec;
                bullet.bulletGameObject = bulletGameObject;
                bullet.bulletTransform = bulletGameObject.transform;

                bulletGameObject.GetComponent<BulletProvider>().bulletEntity = bulletEntity;
                //bulletGameObject.GetComponent<BulletProvider>()._world = _ecsWorld;
                
                weaponEntity.Get<WeaponDelayShootingRequest>().Timer = weapon.bulletsIntervalTimeSec;
                bulletEntity.Get<BulletDestroyWithDelayRequest>().Timer = bullet.bulletsDeactivationTimeSec;
            }
        }
    }
}