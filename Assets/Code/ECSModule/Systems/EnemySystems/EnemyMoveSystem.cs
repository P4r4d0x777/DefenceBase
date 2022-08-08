using Code.ECSModule.Components;
using Code.ECSModule.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECSModule.Systems.EnemySystems
{
    public class EnemyMoveSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyHeroComponent, PlayerOnPlayerBaseEvent> moveToBaseFilter;
        private EcsFilter<EnemyHeroComponent, PlayerOnEnemyBaseEvent> moveToPlayerFilter;
        public void Run()
        {
            foreach (var i in moveToBaseFilter)
            {
                ref var enemy = ref moveToBaseFilter.Get1(i);
                enemy.Agent.destination = new Vector3(0, 0, 20);
            }   
            
            foreach (var i in moveToPlayerFilter)
            {
                ref var enemy = ref moveToPlayerFilter.Get1(i);
                enemy.Transform.LookAt(enemy.target);
                enemy.Agent.destination = enemy.target.position;
            }
        }
    }
}