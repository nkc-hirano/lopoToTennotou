using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace GameCore
{
    public class StageDataController : MonoBehaviour, IStageInitializable
    {
        Direction[,] currentMoveGimmickData = null;
        Direction[,] currentButtonGimmickData = null;
        SpuareOptionFlag[,] currentOtherGimmickData = null;
        StageDataUpdateLogicDef logicDef = new StageDataUpdateLogicDef();
        Subject<StageDataUpdateResultData> updateResultChangeSubject = new Subject<StageDataUpdateResultData>();

        public IObservable<StageDataUpdateResultData> UpdateResultChangeObserbable => updateResultChangeSubject;

        public void StageDataRegister(Direction[,] moveGimmickData, Direction[,] goalGimmickData, SpuareOptionFlag[,] otherGimmickData)
        {
            currentMoveGimmickData = moveGimmickData;
            currentButtonGimmickData = goalGimmickData;
            currentOtherGimmickData = otherGimmickData;
        }

        public void StageDataClear()
        {
            currentMoveGimmickData = null;
            currentButtonGimmickData = null;
            currentOtherGimmickData = null;
        }

        public void StageDataUpdate(in StageDataUpdateData stageDataUpdateData)
        {
            if (currentMoveGimmickData == null) { return; }
            logicDef.StageDataCalculation(ref currentMoveGimmickData,
                ref currentButtonGimmickData,
                ref currentOtherGimmickData,
                stageDataUpdateData,
                out StageDataUpdateResultData resultData);
            updateResultChangeSubject.OnNext(resultData);
        }
    }
}
