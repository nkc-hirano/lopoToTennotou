using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public static class StageDataFileFormatExtension
    {
        public static void GetParallelData(this StageDataFileFormat fileFormat,
            out Direction[,] moveGimmickDirections,
            out Direction[,] goalGimmickDirections,
            out SpuareOptionFlag[,] spuareOptionFlags)
        {
            moveGimmickDirections = new Direction[fileFormat.xLength, fileFormat.yLength];
            goalGimmickDirections = new Direction[fileFormat.xLength, fileFormat.yLength];
            spuareOptionFlags = new SpuareOptionFlag[fileFormat.xLength, fileFormat.yLength];

            for (int i = 0; i < fileFormat.xLength; i++)
            {
                for (int j = 0; j < fileFormat.yLength; j++)
                {
                    var refNum = i * 15 + j;
                    moveGimmickDirections[i, j] = fileFormat.moveGimmickDirections[refNum];
                    goalGimmickDirections[i,j] = fileFormat.goalGimmickDirections[refNum];
                    spuareOptionFlags[i,j] = fileFormat.spuareOptionFlags[refNum];
                }
            }
        }
    }
}
