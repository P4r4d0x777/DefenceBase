using Leopotam.Ecs;

namespace Code.ECSModule.Events
{
    public struct EnemyDoDamageEvent
    {
        public EcsEntity target;
        public float damage;
    }
}