using System.Collections.Generic;
using Code.DataModule;
using Code.ECSModule.Components;
using Code.LogicModule;
using Code.ProvidersModule;
using Code.ServiceModule;
using Code.UIModule;
using Code.ViewModule;
using CodeBase.Hero;
using Leopotam.Ecs;
using NTC.Global.Pool;
using UnityEngine;

namespace Code.ECSModule.Systems.EntryPoint
{
    public class GameInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private Configuration _configuration;
        private SceneData _sceneData;
        public void Init()
        {

            EcsEntity playerEntity = _ecsWorld.NewEntity();

            ref var player = ref playerEntity.Get<PlayerHeroComponent>();
            ref var playerInput = ref playerEntity.Get<PlayerInputComponent>();
            ref var playerRotation = ref playerEntity.Get<PlayerRotationComponent>();
            
            GameObject playerGameObject = NightPool.Spawn(_configuration.playerPrefab, _sceneData.playerSpawnPoint.position,
                Quaternion.identity);
            playerGameObject.name = "Player";
            
            player.MoveTransform = playerGameObject.GetComponent<PlayerView>().MoveTransform;
            player.Speed = playerGameObject.GetComponent<PlayerView>().Speed;
            player.PlayerGameObject = playerGameObject;
            player.animator = playerGameObject.GetComponent<PlayerAnimator>();      
            
            playerEntity.Get<PlayerHPComponent>().HP = playerGameObject.GetComponent<PlayerView>().HP;
            playerEntity.Get<PlayerHPComponent>().HPBar = playerGameObject.GetComponent<PlayerView>().HPBar;
            playerEntity.Get<PlayerHPComponent>().HpGameObject = playerGameObject.GetComponent<PlayerView>().HpGameObject;
            
            EcsEntity weaponEntity = _ecsWorld.NewEntity();
            ref var weapon = ref weaponEntity.Get<WeaponComponent>();

            weapon.bulletParent = playerGameObject.GetComponentInChildren<WeaponView>().bulletParent;
            weapon.bulletPrefab = playerGameObject.GetComponentInChildren<WeaponView>().bulletPrefab;
            weapon.bulletSpeed = playerGameObject.GetComponentInChildren<WeaponView>().bulletSpeed;
            weapon.weaponDamage = playerGameObject.GetComponentInChildren<WeaponView>().weaponDamage;
            weapon.bulletsIntervalTimeSec = playerGameObject.GetComponentInChildren<WeaponView>().bulletsIntervalTimeSec;
            weapon.bulletsDeactivationTimeSec = playerGameObject.GetComponentInChildren<WeaponView>().bulletsDeactivationTimeSec;

            playerEntity.Get<HaveWeaponComponent>().weapon = weaponEntity;
            playerEntity.Get<InventoryComponent>().InventoryTransform = playerGameObject.GetComponent<PlayerView>().InventoryTransform;
            playerEntity.Get<InventoryComponent>().lootInInventory = new List<EcsEntity>();
            playerEntity.Get<InventoryComponent>().lastLootNumber = 0;
            //playerEntity.Get<InventoryComponent>().lastLootPlace = 0;
            
            playerRotation.RotateTransform = playerGameObject.GetComponent<PlayerView>().RotateTransform;;
            playerInput.Joystick = _sceneData.playerJoystick;
            
            CameraSetup.Setup(_sceneData.Camera, player.MoveTransform);
            RuntimeData.PlayerOnPlayerBase = true;

            List<EcsEntity> enemies = new List<EcsEntity>();
            enemies = EnemySpawnerService.SpawnEnemies(_configuration.enemyCount, _configuration, ref _ecsWorld, _sceneData, playerEntity);
            RuntimeData.storage = new EntitiesStorageService(enemies, playerEntity);
            //storage = new EntitiesStorageService(enemies, playerEntity, weaponEntity);
            // убрать из плеер тригера и добавить в enemiesstorageservice тут создать это хранилище через конструктор и прокинуть В ТРИГЕР чекер?
            playerGameObject.GetComponent<PlayerProvider>().storage = RuntimeData.storage;


            EcsEntity enemySpawner = _ecsWorld.NewEntity();
            enemySpawner.Get<EnemySpawnerComponent>().enemyFrequency = _configuration.enemyFrequency;
        }

        public void InitializePlayer()
        {
            
        }

        public void InitializeCamera()
        {
            
        }
    }
}