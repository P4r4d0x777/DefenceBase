using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECSModule.Components
{
    public struct InventoryComponent
    {
        public int lastLootNumber;
        public List<EcsEntity> lootInInventory;
        public Transform InventoryTransform;
    }
}