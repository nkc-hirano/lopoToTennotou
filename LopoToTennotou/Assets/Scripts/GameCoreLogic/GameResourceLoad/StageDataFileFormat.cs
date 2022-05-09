using UnityEngine;

namespace GameCore
{
    [System.Serializable]
    public struct StageDataFileFormat
    {
        // 有効な値の配列数
        [SerializeField]
        public int xLength;

        [SerializeField]
        public int yLength;

        // 位置と向き
        [SerializeField]
        public Direction[] moveGimmickDirections;

        [SerializeField]
        public Direction[] buttonGimmickDirections;

        [SerializeField]
        public SpuareOptionFlag[] spuareOptionFlags;
    }
}