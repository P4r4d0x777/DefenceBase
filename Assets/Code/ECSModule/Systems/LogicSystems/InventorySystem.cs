using System.Collections.Generic;
using Code.ECSModule.Components;
using Code.ECSModule.Events;
using Code.UIModule;
using Leopotam.Ecs;
using NTC.Global.Pool;
using UnityEngine;

namespace Code.ECSModule.Systems.LogicSystems
{
    public class InventorySystem : IEcsRunSystem
    {
        private EcsFilter<InventoryComponent, HaveLootComponent, AddLootEvent> inventoryFilter;
        private EcsFilter<InventoryComponent, SaveInventoryLootEvent> inventorySaveFilter;
        private RootOfGUI UI;
        public void Run()
        {
            foreach (var i in inventoryFilter)
            {
                ref var inventory = ref inventoryFilter.Get1(i);
                ref var loots = ref inventoryFilter.Get2(i);
                ref var addEvent = ref inventoryFilter.Get3(i);
                
                addEvent.lootGameObject.transform.parent = inventory.InventoryTransform;
                addEvent.lootGameObject.transform.localPosition = new Vector3(0,0.2f * inventory.lastLootNumber,0);
                addEvent.lootGameObject.transform.localRotation = Quaternion.Euler(0,90,0);
                
                inventory.lootInInventory.Add(loots.loot);
                inventory.lastLootNumber++;
            }

            foreach (var i in inventorySaveFilter)
            {
                ref var inventory = ref inventorySaveFilter.Get1(i);
                
                UI.ChangeMoney(inventory.lootInInventory.Count);

                foreach (var lootEntity in inventory.lootInInventory)
                {
                    NightPool.Despawn(lootEntity.Get<LootComponent>().lootGameObject);
                    lootEntity.Destroy();
                }
                
                inventory.lastLootNumber = 0;
                inventory.lootInInventory = new List<EcsEntity>();
            }
        }
    }
}