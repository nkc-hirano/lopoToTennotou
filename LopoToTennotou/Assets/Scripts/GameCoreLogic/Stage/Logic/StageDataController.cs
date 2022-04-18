using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace GameCore
{
    public class StageDataController : MonoBehaviour
    {
        Direction[,] currentMoveGimmickData = null;
        SquareGoalGimmickType[,] currentGoalGimmickData = null;
        SpuareOptionFlag[,] currentOtherGimmickData = null;
        StageDataUpdateLogicDef logicDef = new StageDataUpdateLogicDef();
        Subject<StageDataUpdateResultData> updateResultChangeSubject = new Subject<StageDataUpdateResultData>();

        public IObservable<StageDataUpdateResultData> updateResultChangeObserbable => updateResultChangeSubject;

        public void StageDataRegister(Direction[,] moveGimmickData, SquareGoalGimmickType[,] goalGimmickData, SpuareOptionFlag[,] otherGimmickData)
        {
            currentMoveGimmickData = moveGimmickData;
            currentGoalGimmickData = goalGimmickData;
            currentOtherGimmickData = otherGimmickData;
        }

        public void StageDataClear()
        {
            currentMoveGimmickData = null;
            currentGoalGimmickData = null;
            currentOtherGimmickData = null;
        }

        public void StageDataUpdate(in StageDataUpdateData stageDataUpdateData)
        {
            if (currentMoveGimmickData == null) { return; }
            logicDef.StageDataCalculation(ref currentMoveGimmickData,
                ref currentGoalGimmickData,
                ref currentOtherGimmickData,
                stageDataUpdateData,
                out StageDataUpdateResultData resultData);
            updateResultChangeSubject.OnNext(resultData);
        }
    }
}
