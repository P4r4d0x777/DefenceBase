using System.Collections.Generic;
using Code.DataModule;
using Code.ECSModule.Components;
using Code.ECSModule.Events;
using Code.ProvidersModule;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ServiceModule
{
    public class EntitiesStorageService
    {
        private readonly List<EcsEntity> enemyEntities;
        private readonly EcsEntity playerEntity;

        public EntitiesStorageService()
        {
            
        }
        public EntitiesStorageService(List<EcsEntity> enemyEntities, EcsEntity playerEntity) : this()
        {
            this.enemyEntities = enemyEntities;
            this.playerEntity = playerEntity;
        }

        private void WeaponStopShoot()
        {
            playerEntity.Get<HaveWeaponComponent>().weapon.Del<WeaponShootEvent>();
        }
        public void RemoveEnemy(EcsEntity enemy)
        {
            enemyEntities.Remove(enemy);
            
            if(enemyEntities.Count == 0)
                WeaponStopShoot();
        }
        public void AddEnemy(EcsEntity enemy)
        {
            enemyEntities.Add(enemy);
        }
        public void AddEnemy(List<EcsEntity> enemies)
        {
            foreach (var enemyEntity in enemies)
            {
                enemyEntities.Add(enemyEntity);
            }
        }
        public List<EcsEntity> EnemyEntities => enemyEntities;

        public EcsEntity PlayerEntity => playerEntity;

        public void CheckPlayerAgroForEnemies()
        {
            if (PlayerEntity.Get<PlayerHeroComponent>().PlayerGameObject
                    .GetComponent<PlayerProvider>().onPlayerBase == false)
            {
                if (EnemyEntities.Count != 0)
                {
                    PlayerEntity.Get<PlayerRotateToEnemyEvent>().TargetToRotate = GetNearestEnemy();

                    if (PlayerEntity.Has<HaveWeaponComponent>())
                    {
                        PlayerEntity.Get<HaveWeaponComponent>().weapon.Get<WeaponShootEvent>();
                    }
                }
            }
        }

        public Transform GetNearestEnemy()
        {
            Transform transform = null;
            float min = 9999;

            for (int i = 0; i < enemyEntities.Count; i++)
            {
                if (Vector3.Distance(enemyEntities[i].Get<EnemyHeroComponent>().Transform.position,
                        playerEntity.Get<PlayerHeroComponent>().MoveTransform.position) < min)
                {
                    min = Vector3.Distance(enemyEntities[i].Get<EnemyHeroComponent>().Transform.position,
                        playerEntity.Get<PlayerHeroComponent>().MoveTransform.position);

                    transform = enemyEntities[i].Get<EnemyHeroComponent>().Transform;
                }
            }

            return transform;
        }
    }
}