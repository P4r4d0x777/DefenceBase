using System;
using Code.DataModule;
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
        public EcsEntity playerEntity;
        private bool onPlayerBase;
        private void Awake()
        {
            onPlayerBase = true;
        }
        public void DoJump()
        {
            if(!playerEntity.Has<PlayerJumping>() && onPlayerBase)
                playerEntity.Get<JumpEvent>();
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 13)
            {
                RuntimeData.PlayerOnPlayerBase = false;
                Debug.Log("Join Enemy Base");
                foreach (var enemy in storage.EnemyEntities)
                {
                    //enemy state machine?
                    enemy.Del<PlayerOnPlayerBaseEvent>();
                    enemy.Get<PlayerOnEnemyBaseEvent>();
                    
                    playerEntity.Del<PlayerStopMovingEvent>();
                    playerEntity.Del<PlayerStartMovingOnPlayerBaseEvent>();
                    
                    playerEntity.Del<PlayerJumping>();
                    enemy.Get<EnemyHeroComponent>().animator.SetRun();
                    onPlayerBase = false;
                }

                if (storage.EnemyEntities.Count != 0)
                {
                    storage.CheckPlayerAgroForEnemies();
                }
            }

            if (other.gameObject.layer == 12)
            {
                Debug.Log("Join Player Base");
                RuntimeData.PlayerOnPlayerBase = true;
                foreach (var enemy in storage.EnemyEntities)
                {
                    enemy.Del<EnemyAttackEvent>();
                    enemy.Del<PlayerOnEnemyBaseEvent>();
                    enemy.Get<PlayerOnPlayerBaseEvent>();
                    enemy.Get<EnemyHeroComponent>().animator.SetWalk();
                    onPlayerBase = true;
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