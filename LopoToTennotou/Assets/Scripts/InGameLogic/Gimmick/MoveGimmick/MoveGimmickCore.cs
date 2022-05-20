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

        Subject<Vector2> movePosSubject = new Subject<Vector2>();
        Subject<Vector2> moveDirSubject = new Subject<Vector2>();

        public IObservable<(byte x, byte y)> CoordObservable => coordProperty;
        public IObservable<Direction> DirObservable => dirProperty;

        public IObservable<Vector2> MovePosOservable => movePosSubject;
        public IObservable<Vector2> MoveDirOservable => moveDirSubject;


        private void Start()
        {
            coordProperty.Value = (2, 2);
            // controller.UpdateResultChangeObserbable.Subscribe(StageUpdateDataReceive);
        }
        private void Update()
        {
            CoordUpdate(3,2);
        }

        private void CoordUpdate(byte x, byte y)
        {
           
            coordProperty.Value = (x, y);
            // Logicへのインターフェイスのメソッドコール

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

        private void Hoge(Vector2 vec2)
        {
            movePosSubject.OnNext(vec2);
        }
    }
}
