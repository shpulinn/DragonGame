using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ChunkSettings", menuName = "Chunks", order = 0)]
    public class ChunkSettings : ScriptableObject
    {
        [Header("Шанс пустой точки спавна от 0 до 1")] [Range(0, 1)]
        public float emptyChance = 0.1f;
        [Header("Шанс спавна монет от 0 до 1")] [Range(0, 1)]
        public float coinChance = .7f;
        [Header("Шанс спавна людей от 0 до 1")] [Range(0, 1)]
        public float humanChance = .5f;
        [Header("Шанс спавна ловушек от 0 до 1")] [Range(0, 1)]
        public float trapChance = .4f;
    }
}