using UnityEngine;
using UnityEngine.UI;

namespace Code.ECSModule.Components
{
    public struct PlayerHeroComponent
    {
        //public Rigidbody rb;
        public GameObject PlayerGameObject;
        public Transform MoveTransform;
        public float Speed;
    }
}