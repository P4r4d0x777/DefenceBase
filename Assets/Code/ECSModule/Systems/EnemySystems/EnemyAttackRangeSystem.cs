using Code.ECSModule.Components;
using Code.ECSModule.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECSModule.Systems.EnemySystems
{
    public class EnemyAttackRangeSystem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<EnemyHeroComponent, PlayerOnEnemyBaseEvent> _filter1;
        private EcsFilter<EnemyHeroComponent, PlayerOnPlayerBaseEvent> _filter2;

        public void Run()
        {
            foreach (var i in _filter1)
            {
                ref var enemy = ref _filter1.Get1(i);
                ref var enemyEntity = ref _filter1.GetEntity(i);

                if (Vector3.Distance(enemy.Transform.position, enemy.target.position) < enemy.meleeAttackDistance)
                {
                    enemyEntity.Get<EnemyAttackEvent>();
                }
                //возможно ли без него?
                else
                {
                    enemyEntity.Del<EnemyAttackEvent>();
                }
            }

            foreach (var i in _filter2)
            {
                ref var enemyEntity = ref _filter2.GetEntity(i);

                enemyEntity.Del<EnemyAttackEvent>();
            }
        }
    }
}