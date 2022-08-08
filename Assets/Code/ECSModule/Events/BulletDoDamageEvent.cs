using Leopotam.Ecs;

namespace Code.ECSModule.Events
{
    public struct BulletDoDamageEvent
    {
        public EcsEntity target;
        public EcsEntity bullet;
    }
}