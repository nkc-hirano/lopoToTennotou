using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public struct StageDataUpdateLogicDef
    {
        public void StageDataCalculation(ref Direction[,] moveGimmickDataData,
            ref Direction[,] buttonGimmickData,
            ref SpuareOptionFlag[,] otherGimmickData,
            in StageDataUpdateData inUpdateData,
            out StageDataUpdateResultData resultData)
        {
            if (moveGimmickDataData[inUpdateData.x, inUpdateData.y] != Direction.Zero)
            {
                resultData = StageDataUpdateResultData.Enpty;
                return;
            }

            int moveSquareNum = Mathf.Min(inUpdateData.rightPower, inUpdateData.leftPower);

            for (int i = 0; i < moveSquareNum; i++)
            {

            }
            resultData = new StageDataUpdateResultData();
        }
    }
}
