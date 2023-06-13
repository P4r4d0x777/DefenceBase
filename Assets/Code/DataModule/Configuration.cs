using UnityEngine;

namespace Code.DataModule
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        [Header("Player")]
        public GameObject playerPrefab;
        public GameObject stepTrace;
        
        [Header("Enemy")] 
        public GameObject enemyPrefab;
        [Tooltip("Count of enemies on spawn")] public int enemyCount;
        [Tooltip("Count of enemies on spawn per Timer")] public int enemyFrequency;

        [Header("Loot")] 
        public GameObject lootPrefab;
    }
}   