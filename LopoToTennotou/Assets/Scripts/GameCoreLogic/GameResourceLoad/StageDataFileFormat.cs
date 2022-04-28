using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    [System.Serializable]
    public struct StageDataFileFormat
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
    }
}
