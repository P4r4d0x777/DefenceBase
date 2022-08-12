using Code.ECSModule.Components;
using Code.ECSModule.Events;
using Code.ServiceModule;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ProvidersModule
{
    // 13 BasePlayer
    // 12 BaseEnemy
    public class PlayerProvider : MonoBehaviour
    {
        public EntitiesStorageService storage;
        public bool onPlayerBase = true;
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 13)
            {
                onPlayerBase = false;
                Debug.Log("Join Enemy Base");
                foreach (var enemy in storage.EnemyEntities)
                {
                    //enemy state machine?
                    enemy.Del<PlayerOnPlayerBaseEvent>();
                    enemy.Get<PlayerOnEnemyBaseEvent>();
                    enemy.Get<EnemyHeroComponent>().animator.SetRun();
                }

                if (storage.EnemyEntities.Count != 0)
                {
                    storage.PlayerEntity.Get<PlayerRotateToEnemyEvent>().TargetToRotate = storage.GetNearestEnemy();
                    
                    if (storage.PlayerEntity.Has<HaveWeaponComponent>())
                    {
                        storage.PlayerEntity.Get<HaveWeaponComponent>().weapon.Get<WeaponShootEvent>();
                    }
                }
            }

            if (other.gameObject.layer == 12)
            {
                Debug.Log("Join Player Base");
                onPlayerBase = true;
                foreach (var enemy in storage.EnemyEntities)
                {
                    enemy.Del<PlayerOnEnemyBaseEvent>();
                    enemy.Get<PlayerOnPlayerBaseEvent>();
                    enemy.Get<EnemyHeroComponent>().animator.SetWalk();
                }

                storage.PlayerEntity.Del<PlayerRotateToEnemyEvent>();
                
                if (storage.PlayerEntity.Has<HaveWeaponComponent>())
                {
                    storage.PlayerEntity.Get<HaveWeaponComponent>().weapon.Del<WeaponShootEvent>();
                }

                if (storage.PlayerEntity.Has<HaveLootComponent>())
                {
                    storage.PlayerEntity.Get<SaveInventoryLootEvent>();
                }
            }
        }
    }
}