using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public interface IStageInitializable
    {
        public void StageDataRegister(Direction[,] moveGimmickData, Direction[,] goalGimmickData, SpuareOptionFlag[,] otherGimmickData);
    }
}
