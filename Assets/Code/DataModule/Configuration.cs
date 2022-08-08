using UnityEngine;

namespace Code.DataModule
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        [Header("Player")]
        public GameObject playerPrefab;

        [Header("Enemy")] 
        public GameObject enemyPrefab;
        [Tooltip("Count of enemies on spawn")] public int enemyCount;

        [Header("Loot")] 
        public GameObject lootPrefab;
    }
}   