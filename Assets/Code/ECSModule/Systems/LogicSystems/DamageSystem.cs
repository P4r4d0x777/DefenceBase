using Code.DataModule;
using Code.ECSModule.Components;
using Code.ECSModule.Events;
using Code.ECSModule.Requests;
using Code.ProvidersModule;
using Leopotam.Ecs;
using NTC.Global.Pool;

namespace Code.ECSModule.Systems.LogicSystems
{
    public class DamageSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyDoDamageEvent> filter;
        private EcsFilter<BulletDoDamageEvent> filter2;
        private EcsWorld _world;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var e = ref filter.Get1(i);
                ref var health = ref e.target.Get<PlayerHPComponent>();

                health.HP -= e.damage;
                health.HpGameObject.SetActive(true);
                health.HPBar.fillAmount = health.HP / 100;
                //если мёртв    
                if (health.HP <= 0)
                {
                    e.target.Get<DeathEvent>();
                }

                if (!filter.IsEmpty())
                    filter.GetEntity(i).Destroy();
            }

            foreach (var i in filter2)
            {
                ref var e = ref filter2.Get1(i);

                ref var health = ref e.target.Get<EnemyHPComponent>();

                health.HP -= e.bullet.Get<BulletComponent>().damage;
                health.HpGameObject.SetActive(true);
                health.HPBar.fillAmount = health.HP / 100;
                //если мёртв    

                if (health.HP <= 0)
                {
                    e.target.Get<DeathEvent>();
                }

                e.bullet.Get<BulletDestroyImmediatelyEvent>();

                if (!filter2.IsEmpty())
                    filter2.GetEntity(i).Destroy();
            }
            // filter2.GetEntity(i).Del<BulletDoDamageEvent>();
        }
    }
}