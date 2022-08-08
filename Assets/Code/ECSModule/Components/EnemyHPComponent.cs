using UnityEngine;
using UnityEngine.UI;

namespace Code.ECSModule.Components
{
    public struct EnemyHPComponent
    {
        public float HP;    
        public Image HPBar;
        public GameObject HpGameObject;
    }
}