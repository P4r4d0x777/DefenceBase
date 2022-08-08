using Code.DataModule;
using Code.ECSModule.Components;
using Code.ECSModule.Events;
using Code.ProvidersModule;
using Code.UIModule;
using Leopotam.Ecs;
using NTC.Global.Pool;

namespace Code.ECSModule.Systems.LogicSystems
{
    public class DeathSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyHeroComponent, DeathEvent> deathFilter;
        private EcsFilter<PlayerHeroComponent, DeathEvent> playerDeathFilter;
        private RootOfGUI UI;
        private EcsWorld _world;
        public void Run()
        {
            foreach (var i in deathFilter)
            {
                ref var enemy = ref deathFilter.GetEntity(i);
                
                // если enemy имел лут, то дропаем его 
                if (enemy.Has<HaveLootComponent>())
                {
                    var lootGameObject = NightPool.Spawn(enemy.Get<HaveLootComponent>().loot.Get<LootComponent>().lootGameObject,
                        enemy.Get<HaveLootComponent>().loot.Get<LootComponent>().spawnTransform.position,
                        enemy.Get<HaveLootComponent>().loot.Get<LootComponent>().spawnTransform.rotation);
                    
                    lootGameObject.GetComponent<LootProvider>().lootEntity = enemy.Get<HaveLootComponent>().loot;
                }
                
                // удаляем enemy из сцены и из хранилища
                NightPool.Despawn(enemy.Get<EnemyHeroComponent>().EnemyGameObject);
                RuntimeData.storage.RemoveEnemy(enemy);
                
                // если в хранилище не осталось enemy, то игрок поварачивается свободно
                if(RuntimeData.storage.EnemyEntities.Count == 0)
                    enemy.Get<EnemyHeroComponent>().player.Del<PlayerRotateToEnemyEvent>();
                
                // если enemy умер и игрок находитя на EnemyBase, находим следующего enemy и поворачиваемся к нему
                if (enemy.Get<EnemyHeroComponent>().player.Has<PlayerRotateToEnemyEvent>())
                    enemy.Get<EnemyHeroComponent>().player.Get<PlayerRotateToEnemyEvent>().TargetToRotate =
                        RuntimeData.storage.GetNearestEnemy();
                
                // удаляем enemy из мира
                enemy.Destroy();
            }

            foreach (var i in playerDeathFilter)
            {
                ref var player = ref playerDeathFilter.Get1(i);
                ref var playerEntity = ref playerDeathFilter.GetEntity(i);
                
                
                NightPool.Despawn(player.PlayerGameObject);
                //playerEntity.Destroy();
                
                _world.Destroy();
                UI.ShowRestartMenu();
            }
        }
    }
}