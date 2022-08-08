using Code.ECSModule.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECSModule.Systems.PlayerSystems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerInputComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var input = ref _filter.Get1(i);

                input.Direction = new Vector3(input.Joystick.Horizontal, 0, input.Joystick.Vertical);
            }
        }
    }
}