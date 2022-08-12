using UnityEngine;

namespace CodeBase.Hero
{
    public class EnemyAnimator : MonoBehaviour
    {
        private static readonly int Walk = Animator.StringToHash("Walking");
        private static readonly int Idle = Animator.StringToHash("DynIdle");
        private static readonly int Run = Animator.StringToHash("Running");
        //public string Actually;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }
        
        public void SetRun()
        {
            _animator.SetTrigger(Run);
        }
        public void SetWalk()
        {
            _animator.SetTrigger(Walk);
        }
        public void SetIdle()
        {
            _animator.SetTrigger(Idle);
        }
    }
}