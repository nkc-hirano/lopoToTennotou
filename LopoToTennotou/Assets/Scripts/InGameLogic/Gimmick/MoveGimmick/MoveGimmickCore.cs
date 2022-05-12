using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using Zenject;

namespace GameCore
{
    public class MoveGimmickCore : MonoBehaviour
    {
        // [Inject]
        StageDataController controller = null;
        ReactiveProperty<(byte x, byte y)> coordProperty = new ReactiveProperty<(byte, byte)>();
        ReactiveProperty<Direction> dirProperty = new ReactiveProperty<Direction>();

        public IObservable<(byte x, byte y)> CoordObservable => coordProperty;
        public IObservable<Direction> DirObservable => dirProperty;

        private void Start()
        {
            controller.UpdateResultChangeObserbable.Subscribe(StageUpdateDataReceive);
        }

        private void CoordUpdate(byte x, byte y)
        {
            coordProperty.Value = (x, y);
        }

        private void DirUpdate(Direction dir)
        {
            dirProperty.Value = dir;
        }

        public void GimmickMove(Direction dir, byte rightPower, byte leftPower)
        {
            controller.StageDataUpdate(new StageDataUpdateData(coordProperty.Value.x,
                coordProperty.Value.y,
                rightPower,
                leftPower,
                dir));
        }

        private void StageUpdateDataReceive(StageDataUpdateResultData stageDataUpdateResultData)
        {
            if (stageDataUpdateResultData.beforeX != coordProperty.Value.x) { return; }
            if (stageDataUpdateResultData.beforeY != coordProperty.Value.y) { return; }

            CoordUpdate(stageDataUpdateResultData.currentX, stageDataUpdateResultData.currentY);
            DirUpdate(stageDataUpdateResultData.currentDir);
        }
    }
}
