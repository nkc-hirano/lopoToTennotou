using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public readonly struct StageDataUpdateResultData
    {
        public readonly byte beforeX;
        public readonly byte beforeY;
        public readonly byte currentX;
        public readonly byte currentY;

        public StageDataUpdateResultData(byte beforeX, byte beforeY, byte currentX, byte currentY)
        {
            this.beforeX = beforeX;
            this.beforeY = beforeY;
            this.currentX = currentX;
            this.currentY = currentY;
        }
    }
}
