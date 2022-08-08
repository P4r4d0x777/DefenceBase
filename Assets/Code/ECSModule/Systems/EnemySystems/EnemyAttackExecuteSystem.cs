using Code.ECSModule.Components;
using Code.ECSModule.Events;
using Code.ECSModule.Requests;
using Leopotam.Ecs;

namespace Code.ECSModule.Systems.EnemySystems
{
    public class EnemyAttackExecuteSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<EnemyHeroComponent, EnemyAttackEvent>.Exclude<EnemyBlockAttackRequest> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var enemy = ref _filter.Get1(i);
                ref var enemyEntity = ref _filter.GetEntity(i);

                //что то делаем во время атаки анимация например
                
                
                ref var damageEntity = ref _world.NewEntity().Get<EnemyDoDamageEvent>();
                damageEntity.target = enemy.player;
                damageEntity.damage = enemy.Damage;
                
                
                enemyEntity.Get<EnemyBlockAttackRequest>().Timer = 2f;
            }
        }
    }
}