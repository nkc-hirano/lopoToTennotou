using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public readonly struct StageDataUpdateData 
    {
        public readonly byte x;
        public readonly byte y;
        public readonly byte rightPower;
        public readonly byte leftPower;
        public readonly Direction dir;

        public StageDataUpdateData(byte x, byte y, byte rightPower, byte leftPower, Direction dir)
        {
            this.x = x;
            this.y = y;
            this.rightPower = rightPower;
            this.leftPower = leftPower;
            this.dir = dir;
        }
    }
}
