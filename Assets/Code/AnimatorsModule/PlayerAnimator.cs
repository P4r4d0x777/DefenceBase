using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Hero
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int Walk = Animator.StringToHash("Walking");
        private static readonly int Idle = Animator.StringToHash("DynIdle");
        private static readonly int Run = Animator.StringToHash("Running");
        private static readonly int Shoot = Animator.StringToHash("RiffleWalk");
                                                                        
        public string CurrentState;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }
        public void SetShoot()
        {
            _animator.SetTrigger(Shoot);
            CurrentState = "Shoot";
        }
        public void SetRun()
        {
            _animator.SetTrigger(Run);
            CurrentState = "Run";
        }
        public void SetWalk()
        {
            _animator.SetTrigger(Walk);
            CurrentState = "Walk";
        }
        public void SetIdle()
        {
            _animator.SetTrigger(Idle);
            CurrentState = "Idle";
        }
    }
}