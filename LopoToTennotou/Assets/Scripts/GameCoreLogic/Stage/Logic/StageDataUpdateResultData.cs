using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public readonly struct StageDataUpdateResultData
    {
        public static StageDataUpdateResultData Enpty => new StageDataUpdateResultData(false);

        public readonly bool isValid;
        public readonly byte beforeX;
        public readonly byte beforeY;
        public readonly byte currentX;
        public readonly byte currentY;
        public readonly Direction currentDir;
        public readonly Direction rotDir;

        public StageDataUpdateResultData(bool isValid, byte beforeX, byte beforeY, byte currentX, byte currentY, Direction currentDir, Direction rotDir)
        {
            this.isValid = isValid;
            this.beforeX = beforeX;
            this.beforeY = beforeY;
            this.currentX = currentX;
            this.currentY = currentY;
            this.currentDir = currentDir;
            this.rotDir = rotDir;
        }

        public StageDataUpdateResultData(bool isValid)
        {
            this.isValid = isValid;
            this.beforeX = default;
            this.beforeY = default;
            this.currentX = default;
            this.currentY = default;
            this.currentDir = default;
            this.rotDir = default;
        }
    }
}
