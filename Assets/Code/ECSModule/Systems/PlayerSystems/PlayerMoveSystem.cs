using Code.ECSModule.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECSModule.Systems.PlayerSystems
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerHeroComponent, PlayerInputComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);
                ref var playerInput = ref _filter.Get2(i);
                
                // player.CharacterController.Move(playerInput.Direction * Time.deltaTime * player.Speed)
                // player.rb.AddForce(playerInput.Direction * Time.deltaTime * player.Speed, ForceMode.Impulse);
                player.MoveTransform.Translate(playerInput.Direction * Time.deltaTime * player.Speed, Space.World);
            }
        }
    }
}