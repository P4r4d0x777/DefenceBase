using Code.DataModule;
using Code.ECSModule.Events;
using Code.ECSModule.Systems.BulletSystems;
using Code.ECSModule.Systems.EnemySystems;
using Code.ECSModule.Systems.LogicSystems;
using Code.ECSModule.Systems.PlayerSystems;
using Code.ECSModule.Systems.WeaponSystems;
using Code.UIModule;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.ECSModule.Systems.EntryPoint
{
    public class EcsEntryPoint : MonoBehaviour
    {
        private EcsWorld ecsWorld;
        private EcsSystems systems;
        public Configuration Configuration;
        public RootOfGUI UI;
        public SceneData SceneData;
        private void Start()
        {
            ecsWorld = new EcsWorld();
            systems = new EcsSystems(ecsWorld);

            systems
                .Add(new GameInitSystem())
                .Add(new PlayerInputSystem())
                .Add(new PlayerMoveSystem())
                .Add(new PlayerRotationSystem())
                .Add(new EnemyMoveSystem())
                .Add(new EnemyAttackRangeSystem())
                .Add(new EnemyAttackExecuteSystem())
                .Add(new EnemyAttackBlockSystem())
                .Add(new EnemySpawnSystem())
                .Add(new EnemySpawnBlockSystem())
                .Add(new DamageSystem())
                .Add(new WeaponShootSystem())
                .Add(new WeaponBlockShootSystem())
                .Add(new BulletMoveSystem())
                .Add(new BulletDestroySystem())
                .Add(new InventorySystem())
                .Add(new DeathSystem())
                //.OneFrame<PlayerOnPlayerBaseEvent>()
                .OneFrame<AddLootEvent>()
                .OneFrame<SaveInventoryLootEvent>()
                .OneFrame<DeathEvent>()
                .Inject(Configuration)
                .Inject(SceneData)
                .Inject(ecsWorld)
                .Inject(UI);

            systems?.Init();
        }

        private void Update()
        {
            systems?.Run();
        }

        private void OnDestroy()
        {
            systems?.Destroy();
            
            systems = null;
            
            if (ecsWorld.IsAlive())
            {
                ecsWorld?.Destroy();
            }
            ecsWorld = null;
        }
    }
}