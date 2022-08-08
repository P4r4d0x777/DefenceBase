using UnityEngine;

namespace Code.LogicModule
{
    public static class LootRandomizer
    {
        private static float minRange = 0;
        private static float maxRange = 1;
        
        //Range 0 - 1: chance 80%
        private static float maxChance = 0.8f;
        
        public static bool WillHaveLoot()
        {
            return Random.Range(minRange, maxRange) <= maxChance ? true : false;
        }
    }
}