using UnityEngine;
using System;

namespace GameCore
{
    [System.Serializable]
    public struct StageDataFileFormat : IEquatable<StageDataFileFormat>
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

        public override bool Equals(object obj)
        {
            if (obj is StageDataFileFormat fileData)
            {
                if (fileData.Equals(this))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool Equals(StageDataFileFormat other)
        {
            return this.GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return moveGimmickDirections.GetHashCode() * goalGimmickDirections.GetHashCode() * spuareOptionFlags.GetHashCode();
        }

        public override string ToString()
        {
            return $"xLength:{xLength},yLength:{yLength},moveGimmickDirections:{moveGimmickDirections},goalGimmickDirections:{goalGimmickDirections},spuareOptionFlags:{spuareOptionFlags}";
        }
    }
}