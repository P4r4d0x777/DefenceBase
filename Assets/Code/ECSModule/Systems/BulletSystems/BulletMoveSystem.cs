using Code.ECSModule.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECSModule.Systems.BulletSystems
{
    public class BulletMoveSystem : IEcsRunSystem
    {
        private EcsFilter<BulletComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var bullet = ref _filter.Get1(i);
                ref var bulletEntity = ref _filter.GetEntity(i);
                
                //bullet.bulletGameObject.transform.parent = null;

                bullet.bulletTransform.Translate(Vector3.forward * bullet.speed * Time.deltaTime);
            }   
        }
    }
}