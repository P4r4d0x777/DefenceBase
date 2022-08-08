using System;
using Code.ECSModule.Components;
using Code.ECSModule.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ProvidersModule
{
    // 10 Player
    public class LootProvider : MonoBehaviour
    {
        public EcsEntity lootEntity;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 10)
            {
                lootEntity.Get<LootComponent>().lootGameObject = this.gameObject;
                
                other.GetComponent<PlayerProvider>().storage.PlayerEntity.Get<HaveLootComponent>().loot = lootEntity;
                other.GetComponent<PlayerProvider>().storage.PlayerEntity.Get<AddLootEvent>().lootGameObject = this.gameObject;
            }
        }
    }
}