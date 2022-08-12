using Code.ECSModule.Components;
using Code.ECSModule.Events;
using Leopotam.Ecs;

namespace Code.ECSModule.Systems.PlayerSystems
{
    public class PlayerAnimatorSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerHeroComponent, PlayerStartMovingOnEnemyBaseEvent> filter1;
        private EcsFilter<PlayerHeroComponent, PlayerStartMovingOnPlayerBaseEvent> filter2;
        private EcsFilter<PlayerHeroComponent, PlayerStopMovingEvent> filter3;
        public void Run()
        {
            foreach (var i in filter1)
            {
                ref var player = ref filter1.Get1(i);
                player.animator.SetShoot();
                
                filter1.GetEntity(i).Del<PlayerStartMovingOnEnemyBaseEvent>();
            }
            foreach (var i in filter2)
            {
                ref var player = ref filter2.Get1(i);
                player.animator.SetRun();
                
                filter2.GetEntity(i).Del<PlayerStartMovingOnPlayerBaseEvent>();
            }
            foreach (var i in filter3)
            {
                ref var player = ref filter3.Get1(i);
                player.animator.SetIdle();
                
                filter3.GetEntity(i).Del<PlayerStopMovingEvent>();
            }
        }
    }
}