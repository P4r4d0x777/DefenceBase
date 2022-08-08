using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Code.ECSModule.Components
{
    public struct EnemyHeroComponent
    {
        public GameObject EnemyGameObject;
        public Transform Transform;
        public Transform target;
        public EcsEntity player;
        public float Damage;
        public NavMeshAgent Agent;
        public float meleeAttackDistance;
    }
}