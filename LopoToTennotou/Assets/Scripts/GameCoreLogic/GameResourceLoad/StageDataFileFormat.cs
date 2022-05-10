using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameCore
{
    [System.Serializable]
    public struct StageDataFileFormat : IEquatable<StageDataFileFormat>
    {
        [SerializeField]
        public int xLength;

        [SerializeField]
        public int yLength;

        [SerializeField]
        public Direction[] moveGimmickDirections;

        [SerializeField]
        public Direction[] goalGimmickDirections;

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
