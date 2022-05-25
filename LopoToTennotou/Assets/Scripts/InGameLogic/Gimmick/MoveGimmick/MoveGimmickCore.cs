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

        Subject<Vector3> movePosSubject = new Subject<Vector3>();
        Subject<Vector2> moveDirSubject = new Subject<Vector2>();

        public IObservable<(byte x, byte y)> CoordObservable => coordProperty;
        public IObservable<Direction> DirObservable => dirProperty;

        public IObservable<Vector3> MovePosOservable => movePosSubject;
        public IObservable<Vector2> MoveDirOservable => moveDirSubject;

        StageInstantiater stageInstantiater = new StageInstantiater();

        // 入力側
        [Inject]
        IInputConverterToVecter inputConverterToVecter;

        // 出力側
        [Inject]
        IOutputConverterToVecter outputConverterToVecter;

        private void Start()
        {
            outputConverterToVecter.ConvertResultObservable
                .Select(val => val.pos) 
                .Subscribe(movePosSubject);
            Debug.Log("  gagag");
            stageInstantiater.Hoge(gameObject.name,out byte x, out byte y, out int mapScale);
            CoordUpdate(x, y, mapScale);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
                CoordUpdate(1, 2, 15);
        }

        private void CoordUpdate(byte x, byte y,int mapScale)
        {
            coordProperty.Value = (x, y);
            // Logicへのインターフェイスのメソッドコール
            inputConverterToVecter.ConvertVec(new InputStructConverterToVecter(x,y, mapScale));
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

           // CoordUpdate(stageDataUpdateResultData.currentX, stageDataUpdateResultData.currentY);
            DirUpdate(stageDataUpdateResultData.currentDir);
        }

        private void Hoge(Vector2 vec2)
        {
            movePosSubject.OnNext(vec2);
        }
    }
}
