using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public struct StageDataUpdateLogicDef
    {
        public void StageDataCalculation(ref Direction[,] stageData,
            ref SquareGoalGimmickType[,] goalGimmickData,
            ref SpuareOptionFlag[,] otherGimmickData,
            in StageDataUpdateData inUpdateData,
            out StageDataUpdateResultData resultData)
        {
            resultData = new StageDataUpdateResultData();
        }
    }
}
