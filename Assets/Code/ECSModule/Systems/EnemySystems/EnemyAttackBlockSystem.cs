using Code.ECSModule.Requests;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECSModule.Systems.EnemySystems
{
    public class EnemyAttackBlockSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyBlockAttackRequest> filter;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var entity = ref filter.GetEntity(i);
                ref var block = ref filter.Get1(i);
                
                block.Timer -= Time.deltaTime;
                
                if (block.Timer <= 0)
                {
                    entity.Del<EnemyBlockAttackRequest>();
                }
            }
        }
    }
}