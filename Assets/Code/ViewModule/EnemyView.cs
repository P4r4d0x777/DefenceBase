using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Code.ViewModule
{
    public class EnemyView : MonoBehaviour
    {
        public GameObject EnemyGameObject;
        public Transform LootTransform;
        public float HP;
        public Image HPBar;
        public GameObject HpGameObject;
        public float Damage;
        public NavMeshAgent Agent;
        public float meleeAttackDistance;
    }
}