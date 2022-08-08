using Code.ECSModule.Components;
using Code.ECSModule.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECSModule.Systems.PlayerSystems
{
    public class PlayerRotationSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerHeroComponent, PlayerInputComponent, PlayerRotationComponent>.Exclude<PlayerRotateToEnemyEvent> _filter1;
        private EcsFilter<PlayerHeroComponent, PlayerInputComponent, PlayerRotationComponent, PlayerRotateToEnemyEvent> _filter2;
        
        public void Run()
        {
            foreach (var i in _filter1)
            {
                ref var player = ref _filter1.Get1(i);
                ref var playerInput = ref _filter1.Get2(i);
                ref var playerRotation = ref _filter1.Get3(i);

                
                    if (playerInput.Joystick.Horizontal != 0 || playerInput.Joystick.Vertical != 0)
                        playerRotation.RotateTransform.rotation = Quaternion.LookRotation(playerInput.Direction);
            }
            
            foreach (var i in _filter2)
            {
                ref var player = ref _filter2.Get1(i);
                ref var playerInput = ref _filter2.Get2(i);
                ref var playerRotation = ref _filter2.Get3(i);
                ref var playerRotateEvent = ref _filter2.Get4(i);


                playerRotation.RotateTransform.LookAt(playerRotateEvent.TargetToRotate);
            }
        }
    }
}