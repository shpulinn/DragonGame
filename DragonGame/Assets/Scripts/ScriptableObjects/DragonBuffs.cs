using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "DragonBuff", menuName = "DragonBuffs", order = 0)]
    public class DragonBuffs : ScriptableObject
    {
        [Header("Parameters")] 
        public string buffName;
        public Sprite buffIcon;
        public float buffDuration;
        [Header("Money")]
        public bool enableCoinsMultiplier;
        public int coinsMultiplier;
        public bool enableDiamonds;
        [Space] [Header("People")] 
        public bool enableMoveThroughPeople;

        [Space] [Header("Obstacles")] 
        public bool enableMoveThroughTraps;
        public bool enableBreakingObstacles;

        [Space] [Header("Other")] 
        public bool enableSpawningSpecialChunk;
    }
}