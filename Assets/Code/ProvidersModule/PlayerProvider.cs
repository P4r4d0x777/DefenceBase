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
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 13)
            {
                Debug.Log("Join Enemy Base");
                foreach (var enemy in storage.EnemyEntities)
                {
                    //enemy state machine?
                    enemy.Del<PlayerOnPlayerBaseEvent>();
                    enemy.Get<PlayerOnEnemyBaseEvent>();
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

                foreach (var enemy in storage.EnemyEntities)
                {
                    enemy.Del<PlayerOnEnemyBaseEvent>();
                    enemy.Get<PlayerOnPlayerBaseEvent>();
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