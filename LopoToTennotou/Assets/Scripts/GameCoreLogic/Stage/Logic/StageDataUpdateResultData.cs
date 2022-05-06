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
        public readonly Direction currentDir;

        public StageDataUpdateResultData(byte beforeX, byte beforeY, byte currentX, byte currentY, Direction currentDir)
        {
            this.beforeX = beforeX;
            this.beforeY = beforeY;
            this.currentX = currentX;
            this.currentY = currentY;
            this.currentDir = currentDir;
        }
    }
}
