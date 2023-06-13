using Code.ECSModule.Components;
using Code.ECSModule.Events;
using Leopotam.Ecs;

namespace Code.ECSModule.Systems.PlayerSystems
{
    public class PlayerAnimatorSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerHeroComponent, PlayerStartMovingOnEnemyBaseEvent>.Exclude<PlayerJumping> filter1;
        private EcsFilter<PlayerHeroComponent, PlayerStartMovingOnPlayerBaseEvent>.Exclude<PlayerJumping> filter2;
        private EcsFilter<PlayerHeroComponent, PlayerStopMovingEvent>.Exclude<PlayerJumping> filter3;
        private EcsFilter<PlayerHeroComponent, JumpEvent> filter4;
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
            foreach (var i in filter4)
            {
                ref var player = ref filter4.Get1(i);
                player.animator.SetJump();
                
                ref var entity = ref filter4.GetEntity(i);
                entity.Get<PlayerJumping>();
                
                filter4.GetEntity(i).Del<JumpEvent>();
            }
        }
    }
}