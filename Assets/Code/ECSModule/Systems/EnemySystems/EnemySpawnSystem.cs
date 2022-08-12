using System.Collections.Generic;
using Code.DataModule;
using Code.ECSModule.Components;
using Code.ECSModule.Events;
using Code.ECSModule.Requests;
using Code.ProvidersModule;
using Code.ServiceModule;
using Leopotam.Ecs;

namespace Code.ECSModule.Systems.EnemySystems
{
    public class EnemySpawnSystem : IEcsRunSystem
    {
        private EcsFilter<EnemySpawnerComponent>.Exclude<EnemySpawnerBlockRequest> filter;
        private Configuration _configuration;
        private EcsWorld _ecsWorld;
        private SceneData _sceneData;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var spawner = ref filter.Get1(i);
                ref var spawnerEntity = ref filter.GetEntity(i);
                
                List<EcsEntity> enemies;

                enemies = EnemySpawnerService.SpawnEnemies(spawner.enemyFrequency, _configuration, ref _ecsWorld, 
                    _sceneData, RuntimeData.storage.PlayerEntity);
                
                RuntimeData.storage.AddEnemy(enemies);
                

                RuntimeData.storage.CheckPlayerAgroForEnemies();

                spawnerEntity.Get<EnemySpawnerBlockRequest>().Timer = 5f;
            }
        }
    }
}