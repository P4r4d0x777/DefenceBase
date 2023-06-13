using Code.DataModule;
using Code.ECSModule.Components;
using Code.ECSModule.Events;
using Code.ProvidersModule;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECSModule.Systems.PlayerSystems
{
    public class PlayerRotationSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerHeroComponent, PlayerInputComponent, PlayerRotationComponent>.Exclude<
            PlayerRotateToEnemyEvent> _filter1;

        private EcsFilter<PlayerHeroComponent, PlayerInputComponent, PlayerRotationComponent, PlayerRotateToEnemyEvent>
            _filter2;

        public void Run()
        {
            foreach (var i in _filter1)
            {
                ref var playerEntity = ref _filter1.GetEntity(i);
                ref var player = ref _filter1.Get1(i);
                ref var playerInput = ref _filter1.Get2(i);
                ref var playerRotation = ref _filter1.Get3(i);
                
                
                Debug.Log(playerInput.Joystick.Horizontal);
                Debug.Log(playerInput.Joystick.Vertical);
                if (playerInput.Joystick.Horizontal != 0 || playerInput.Joystick.Vertical != 0)
                {
                    playerRotation.RotateTransform.rotation = Quaternion.LookRotation(playerInput.Direction);

                    if (RuntimeData.PlayerOnPlayerBase == true && player.animator.CurrentState != "Run")
                        playerEntity.Get<PlayerStartMovingOnPlayerBaseEvent>();

                    if (RuntimeData.PlayerOnPlayerBase == false && player.animator.CurrentState != "Shoot")
                        playerEntity.Get<PlayerStartMovingOnEnemyBaseEvent>();
                }
                else
                {
                    if (player.animator.CurrentState != "Idle")
                        playerEntity.Get<PlayerStopMovingEvent>();
                }
            }

            foreach (var i in _filter2)
            {
                ref var playerEntity = ref _filter1.GetEntity(i);
                ref var player = ref _filter2.Get1(i);
                ref var playerInput = ref _filter2.Get2(i);
                ref var playerRotation = ref _filter2.Get3(i);
                ref var playerRotateEvent = ref _filter2.Get4(i);

                if (RuntimeData.PlayerOnPlayerBase == false && player.animator.CurrentState != "Shoot")
                    playerEntity.Get<PlayerStartMovingOnEnemyBaseEvent>();

                else if (RuntimeData.PlayerOnPlayerBase == false && RuntimeData.storage.EnemyEntities.Count == 0 && player.animator.CurrentState != "Idle")
                    playerEntity.Get<PlayerStopMovingEvent>();


                playerRotation.RotateTransform.LookAt(playerRotateEvent.TargetToRotate);
            }
        }
    }
}