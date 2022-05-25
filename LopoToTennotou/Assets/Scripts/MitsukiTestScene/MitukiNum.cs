using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using GameCore;
using Zenject;

namespace MitukiTest
{
    enum MitukiNum
    {
        Start,
        Goal,
        Indestructible,
        Wall,
        WallSide,
        MoveObj,
        ButtonObj,
        ButtonObj2,
        PitTrap,
        Dark,
        CannonTrapObj,
    }
    enum MitukiNum3
    {
        Zero = 1,
        Up = 2,
        Down = 4,
        Right = 8,
        Left = 16,
    }
    public enum Mitukinum2
    {
        // なんか増える
        PitTrap = 1,
        Dark = 2,
        Start = 4,
        Goal = 8,
        Indestructible = 16,
        CannonTrapUp = 32,
        CannonTrapDown = 64,
        CannonTrapLeft = 128,
        CannonTrapRight = 256,
        WallUp = 512,
        WallDown = 1024,
        WallLeft = 2048,
        WallRight = 4096,
        WallLeftUp = 8192,
        WallRightDown = 16384,
        WallRightUp = 32768,
        WallLeftDown = 65536,
    }
    [System.Serializable]
    public struct MitukiStageDataFileFormat : IEquatable<MitukiStageDataFileFormat>
    {
        // 有効な値の配列数
        [SerializeField]
        public int xLength;

        [SerializeField]
        public int yLength;

        // 位置と向き
        [SerializeField]
        public Direction[] moveGimmickDirections;

        [SerializeField]
        public Direction[] buttonGimmickDirections;

        [SerializeField]
        public Mitukinum2[] spuareOptionFlags;

        public override bool Equals(object obj)
        {
            if (obj is MitukiStageDataFileFormat fileData)
            {
                if (fileData.Equals(this))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool Equals(MitukiStageDataFileFormat other)
        {
            return this.GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return moveGimmickDirections.GetHashCode() * buttonGimmickDirections.GetHashCode() * spuareOptionFlags.GetHashCode();
        }

        public override string ToString()
        {
            return $"xLength:{xLength},yLength:{yLength},moveGimmickDirections:{moveGimmickDirections},goalGimmickDirections:{buttonGimmickDirections},spuareOptionFlags:{spuareOptionFlags}";
        }
    }
    [System.Serializable]
    struct NameStageObjPair
    {
        public string name;
        public GameObject obj;
    }
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
    public class StageDataLoaderBase : MonoBehaviour
    {
        [Inject]
        protected IStageInitializable initializer;
    }
    public class StageDateLoader : StageDataLoaderBase
    {
        private static Dictionary<int, StageDataFileFormat> fileDataDic = new Dictionary<int, StageDataFileFormat>();
    }
}