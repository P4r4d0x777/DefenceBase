using System.Collections.Generic;
using Code.DataModule;
using Code.ECSModule.Components;
using Code.ECSModule.Events;
using Code.LogicModule;
using Code.ProvidersModule;
using Code.ViewModule;
using Leopotam.Ecs;
using NTC.Global.Pool;
using UnityEngine;

namespace Code.ServiceModule
{
    public  static class EnemySpawnerService
    {
        public static List<EcsEntity> SpawnEnemies(int countEnemies, Configuration configuration, ref EcsWorld ecsWorld, SceneData sceneData, EcsEntity playerEntity)
        {
            List<EcsEntity> enemies = new List<EcsEntity>();
            
            for (int i = 0; i < countEnemies; i++)
            {
                EcsEntity enemyEntity = ecsWorld.NewEntity();
                

                GameObject enemyGameObject = NightPool.Spawn(configuration.enemyPrefab, MeshPlaneRandomizer.GetSpawnPositionEnemy(sceneData.EnemyBasePlaneMesh));
                enemyGameObject.GetComponent<EnemyProvider>().enemyEntity = enemyEntity;
                enemyGameObject.GetComponent<EnemyProvider>()._world = ecsWorld;
                
                ref var enemy = ref enemyEntity.Get<EnemyHeroComponent>();
                
                enemy.Agent = enemyGameObject.GetComponent<EnemyView>().Agent;
                enemy.EnemyGameObject = enemyGameObject.GetComponent<EnemyView>().EnemyGameObject;
                enemy.target = playerEntity.Get<PlayerHeroComponent>().MoveTransform;
                enemy.player = playerEntity;
                enemy.Damage = enemyGameObject.GetComponent<EnemyView>().Damage;
                enemy.Transform = enemyGameObject.transform;
                enemy.meleeAttackDistance = enemyGameObject.GetComponent<EnemyView>().meleeAttackDistance;
                
                
                enemyEntity.Get<EnemyHPComponent>().HP = enemyGameObject.GetComponent<EnemyView>().HP;
                enemyEntity.Get<EnemyHPComponent>().HPBar = enemyGameObject.GetComponent<EnemyView>().HPBar;
                enemyEntity.Get<EnemyHPComponent>().HpGameObject = enemyGameObject.GetComponent<EnemyView>().HpGameObject;

                if (LootRandomizer.WillHaveLoot())
                {
                    EcsEntity lootEntity = ecsWorld.NewEntity();
                    
                    lootEntity.Get<LootComponent>().spawnTransform = enemyGameObject.GetComponent<EnemyView>().LootTransform;
                    lootEntity.Get<LootComponent>().lootGameObject = configuration.lootPrefab;
                    
                    enemyEntity.Get<HaveLootComponent>().loot = lootEntity;
                }
                
                if(playerEntity.Get<PlayerHeroComponent>().PlayerGameObject.GetComponent<PlayerProvider>().onPlayerBase)
                    enemyEntity.Get<PlayerOnPlayerBaseEvent>();
                else
                    enemyEntity.Get<PlayerOnEnemyBaseEvent>();
                
                enemies.Add(enemyEntity);
            }

            return enemies;
        }
    }
}