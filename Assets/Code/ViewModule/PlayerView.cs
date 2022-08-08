using UnityEngine;
using UnityEngine.UI;

namespace Code.ViewModule
{
    public class PlayerView : MonoBehaviour
    {
        public Transform MoveTransform;
        public Transform RotateTransform;
        public Transform InventoryTransform;
        public float Speed;
        public float HP;
        public Image HPBar;
        public GameObject HpGameObject;
        public Rigidbody rb;
    }
}