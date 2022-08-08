using Code.ECSModule.Components;
using Code.ECSModule.Requests;
using Leopotam.Ecs;
using NTC.Global.Pool;
using UnityEngine;

namespace Code.ECSModule.Systems.BulletSystems
{
    public class BulletDestroySystem : IEcsRunSystem
    {
        private EcsFilter<BulletComponent, BulletDestroyWithDelayRequest> _filter1;
        private EcsFilter<BulletComponent, BulletDestroyImmediatelyEvent> _filter2;

        public void Run()
        {
            foreach (var i in _filter1)
            {
                ref var entity = ref _filter1.GetEntity(i);
                ref var bullet = ref _filter1.Get1(i);
                ref var bulletDestroy = ref _filter1.Get2(i);

                bulletDestroy.Timer -= Time.deltaTime;

                if (bulletDestroy.Timer <= 0)
                {
                    // проверка на null
                    NightPool.Despawn(bullet.bulletGameObject);
                    entity.Destroy();
                }
            }

            foreach (var i in _filter2)
            {
                ref var entity = ref _filter2.GetEntity(i);
                ref var bullet = ref _filter2.Get1(i);
                
                //проверка на null
                NightPool.Despawn(bullet.bulletGameObject);
                entity.Destroy();
            }
        }
    }
}