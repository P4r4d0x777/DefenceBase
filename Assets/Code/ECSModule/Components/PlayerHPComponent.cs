using UnityEngine;
using UnityEngine.UI;

namespace Code.ECSModule.Components
{
    public struct PlayerHPComponent
    {
        public float HP;    
        public Image HPBar;
        public GameObject HpGameObject;
    }
}