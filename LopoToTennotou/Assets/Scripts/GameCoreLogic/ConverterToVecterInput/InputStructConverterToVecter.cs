using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public struct InputStructConverterToVecter
    {
        public byte x; // 今の座標x
        public byte y; // 今の座標y

        public int mapScale; // マップの大きさ

        public InputStructConverterToVecter(byte x, byte y, int mapScale)
        {
            this.x = x;
            this.y = y;
            this.mapScale = mapScale;
        }
    }
}
