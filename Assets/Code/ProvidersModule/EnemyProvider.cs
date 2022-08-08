using Code.ECSModule.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ProvidersModule
{
    // 15 Bullet
    public class EnemyProvider : MonoBehaviour
    {
        public EcsEntity enemyEntity;
        public EcsWorld _world;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 15)
            {
                // Debug.Log("hit");
                
                ref var damageEntity = ref _world.NewEntity().Get<BulletDoDamageEvent>();
                
                damageEntity.target = enemyEntity;
                damageEntity.bullet = other.gameObject.GetComponent<BulletProvider>().bulletEntity;
                
            }
        }
    }
}